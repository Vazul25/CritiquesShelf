using CritiquesShelfBLL.RepositoryInterfaces;
using CritiquesShelfBLL.Entities;
using CritiquesShelfBLL.ViewModels;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CritiquesShelfBLL.Utility;

namespace CritiquesShelfBLL.Managers
{
    public class BookManager : RepositoryBase<Book>, IBookRepository
    {

        public BookManager(CritiquesShelfDbContext context) : base(context)
        {

        }

        public List<Author> GetAuthors()
        {
            return _context.Authors.ToList();
        }

        public PagedData<List<BookProposalModel>> GetBookProposals(int page, int pageSize)
        {
            IQueryable<BookProposal> query;

            if (pageSize == 0) query = _context.BookProposals.Include(b => b.Proposer).Include(b => b.Tags).OrderBy(b => b.Id);
            else if (page == 0) query = _context.BookProposals.Include(b => b.Proposer).Include(b => b.Tags).OrderBy(b => b.Id).Take(pageSize);
            else query = _context.BookProposals.Include(b => b.Proposer).Include(b => b.Tags).OrderBy(b => b.Id).Skip(pageSize * page).Take(pageSize);
            var data = query.Select(b => new BookProposalModel
            {
                AuthorsNames = b.Authors.Select(a => a.Name).ToList(),
                Description = b.Description,
                Id = b.Id,
                ProposerName = b.Proposer.UserName,
                Tags = b.Tags.Select(t => t.Label).ToList(),
                Title = b.Title
            })?.ToList();

            return new PagedData<List<BookProposalModel>>()
            {
                Page = page,
                PageSize = pageSize,
                Data = data,
                HasNext = data.Count == pageSize ? true : false
            };

        }

        public PagedData<List<BookModel>> GetBooks(int page, int pageSize, List<string> Tags, string searchText)
        {



            IQueryable<Book> query = _context.Books.Include(b => b.Authors).Include(b => b.TagConnectors).ThenInclude(tc => tc.Tag);
            if (!searchText.IsNullOrEmpty())
            {
                query = query.Where(b => b.Title.Contains(searchText) || b.Authors.Any(a => a.Name.Contains(searchText)));
            }
            if (!Tags.IsNullOrEmpty())
            {
                // query = query.Where(b => Tags.All(t => b.TagConnectors.Any(bt => bt.Tag.Label == t)));
                query = query.Where(b => b.TagConnectors.Count(tc => Tags.Contains(tc.Tag.Label)) == Tags.Count);

            }
            if (pageSize == 0) query = query.OrderBy(b => b.Id);
            else query = query.OrderBy(b => b.Id).Skip(pageSize * page).Take(pageSize);
            //var teszt10 = query.ToList();



            var result = new List<BookModel>();
            query.ToList().ForEach(b => result.Add(
            new BookModel
            {
                AuthorsNames = b.Authors.Select(a => a.Name).ToList(),
                Description = (b.Description == null || b.Description.Length < 200) ? b.Description : b.Description.Substring(0, 200),
                Rateing = b.ReviewScore,
                Tags = b.TagConnectors.Select(tc => tc.Tag.Label).ToList(),
                Title = b.Title,
                Cover = b.CoverId,
                DatePublished = b.DatePublished
            }));



            return new PagedData<List<BookModel>>()
            {
                Page = page,
                PageSize = pageSize,
                Data = result,
                HasNext = result.Count == pageSize ? true : false
            };




        }

        public long MakeNewBookProposal(string userId, string title, string description, List<Author> authors, List<string> tags, int? datePublished)
        {
            
            for (int i = 0; i < authors.Count; i++)
            {
                if (authors[i].Id == 0)
                {
                    var newAuthor = new Author { Name = authors[i].Name };
                    //_context.Authors.Add(newAuthor);
                    authors[i] = newAuthor;
                }
            }

            //_context.SaveChanges();

            HashSet<TagProposal> tagsToAdd = new HashSet<TagProposal>();
            tags.ForEach(t =>
            {
                var newTagProposal = new TagProposal { Label = t };
                _context.TagProposals.Add(newTagProposal);
                tagsToAdd.Add(newTagProposal);
            });
             
            var bookProposalToAdd = new BookProposal()
            {
                Title = title,
                Description = description,
                
                Proposer = _context.Users.Find(userId),
                 
                DatePublished = datePublished
            };
            _context.BookProposals.Add(bookProposalToAdd);
            bookProposalToAdd.Authors = authors;
            bookProposalToAdd.Tags = tagsToAdd;
            _context.SaveChanges();
            return bookProposalToAdd.Id;

        }

        BookModel IBookRepository.Find(long id)
        {
            var book = _context.Books.Include(b => b.TagConnectors).ThenInclude(tc => tc.Book).Where(b => b.Id == id).FirstOrDefault();

            return new BookModel()
            {
                AuthorsNames = book.Authors.Select(a => a.Name).ToList(),
                Description = book.Description,
                Rateing = book.ReviewScore,
                Tags = book.TagConnectors.Select(tc => tc.Tag.Label).ToList(),
                Title = book.Title
            };
        }

        public long AddNewReview(long bookId, ReviewModel review) {
            var book = _context.Books.Find(bookId);

            var reviewEntity = new Review
            {
                BookId = bookId,
                Score = review.Score,
                Description = review.Description,
                UserId = review.UserId,
                Date = review.Date
            };

            book.Reviews.Add(reviewEntity);

            _context.SaveChanges();

            return reviewEntity.Id;
        }
    }
}
