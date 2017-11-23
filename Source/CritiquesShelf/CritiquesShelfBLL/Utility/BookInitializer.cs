using System;
using System.Threading.Tasks;
using CritiquesShelfBLL.Entities;
using Google.Apis.Books.v1;
using Google.Apis.Services;
using System.Linq;
using System.Globalization;
using CritiquesShelfBLL.ConnectionTables;
using System.Collections.Generic;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace CritiquesShelfBLL.Utility
{
    public static class BookInitializer
    {

        public static async Task SeedBooks(CritiquesShelfDbContext dbContext, GoogleBooksApiConfig config)
        {
            var hasBooksInDb = dbContext.Books.Any();

            if (hasBooksInDb && config.ForceUpdate)
            {
                DeleteTableContents(dbContext);
            }
            else if (hasBooksInDb)
            {
                return;
            }

            var service = new BooksService(
                new BaseClientService.Initializer
                {
                    ApiKey = config.Key,
                    ApplicationName = "CritiquesShelf"
                });

            var volumesQuery = service.Volumes.List("a");
            volumesQuery.MaxResults = 40;

            var volumes = await volumesQuery.ExecuteAsync();

            for (var i = 0; i < volumes.TotalItems; i += 40)
            {

                foreach (var item in volumes.Items)
                {

                    var tags = dbContext.Tags.Select(x => x.Label.ToUpper()).ToHashSet();

                    var bookEntity = new Book
                    {
                        Title = item.VolumeInfo.Title,
                        Description = item.VolumeInfo.Description,
                        Authors = item.VolumeInfo.Authors?.Select(x => new Author { Name = x }).ToList(),
                        TagConnectors = new HashSet<TagConnector>()
                    };

                    item.VolumeInfo?
                        .Categories?
                        .Distinct()
                        .Where(x => !tags.Contains(x.ToUpper()))
                        .ToList()
                        .ForEach(x =>
                        {
                            var tag = new Tag { Label = x };
                            var tagConnector = new TagConnector { Book = bookEntity, Tag = tag };

                            bookEntity.TagConnectors.Add(tagConnector);
                        });

                    var successParse = DateTime.TryParse(item.VolumeInfo.PublishedDate, out DateTime date);

                    // If its a yyyy-mm-dd formatted string
                    if (successParse)
                    {
                        bookEntity.DatePublished = date.Year;
                    }
                    else
                    {
                        // If its only a yyyy formatted string
                        successParse = int.TryParse(item.VolumeInfo.PublishedDate, out int year);

                        // If its not yyyy or yyyy-mm-dd, then set null for published year
                        bookEntity.DatePublished = successParse ? year : (int?)null;
                    }

                    var link = item.VolumeInfo?.ImageLinks?.ExtraLarge ??
                                   item.VolumeInfo?.ImageLinks?.Large ??
                                   item.VolumeInfo?.ImageLinks?.Medium ??
                                   item.VolumeInfo?.ImageLinks?.Small ??
                                   item.VolumeInfo?.ImageLinks?.Thumbnail ??
                                   item.VolumeInfo?.ImageLinks?.SmallThumbnail;
                    
                    bookEntity.CoverId = link;   

                    dbContext.Books.Add(bookEntity);
                    dbContext.SaveChanges();
                }

                volumesQuery.StartIndex = i+40;
                volumes = await volumesQuery.ExecuteAsync();
            }
        }

        private static void DeleteTableContents(CritiquesShelfDbContext dbContext) {
            dbContext.Database.ExecuteSqlCommand("DELETE FROM dbo.Authors");
            dbContext.Database.ExecuteSqlCommand("DELETE FROM dbo.TagConnector");
            dbContext.Database.ExecuteSqlCommand("DELETE FROM dbo.Books");
            dbContext.Database.ExecuteSqlCommand("DELETE FROM dbo.Tags");
        }
    }
}
