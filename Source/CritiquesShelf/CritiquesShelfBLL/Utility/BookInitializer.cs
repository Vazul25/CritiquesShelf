using System;
using System.Threading.Tasks;
using CritiquesShelfBLL.Entities;
using Google.Apis.Books.v1;
using Google.Apis.Services;
using System.Linq;
using System.Globalization;
using CritiquesShelfBLL.ConnectionTables;
using System.Collections.Generic;

namespace CritiquesShelfBLL.Utility
{
    public static class BookInitializer
    {

        public static async Task SeedBooks(CritiquesShelfDbContext dbContext, string googleBooksApiKey)
        {
            if (dbContext.Books.Any()) 
            {
                return;
            }

            var service = new BooksService(
                new BaseClientService.Initializer
                {
                    ApiKey = googleBooksApiKey,
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

                    dbContext.Books.Add(bookEntity);
                    dbContext.SaveChanges();
                }

                volumesQuery.StartIndex = i+40;
                volumes = await volumesQuery.ExecuteAsync();
            }
        }
    }
}
