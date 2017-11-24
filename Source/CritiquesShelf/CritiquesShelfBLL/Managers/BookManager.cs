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
            return _context.Authors.Select(a=>a.Name).Distinct().ToList().Select(a=>new Author { Id=0,Name=a}).ToList();
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

        public PagedData<List<BookModel>> GetBooks(string userId, int page, int pageSize, List<string> Tags, string searchText)
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


            var bookList = query.ToList();
            var favs = _context.FavouritesConnector.Where(fc => fc.UserId == userId && bookList.Any(b => b.Id == fc.BookId)).Select(f => f.BookId).ToHashSet();
            var read = _context.ReadConnector.Where(fc => fc.UserId == userId && bookList.Any(b => b.Id == fc.BookId)).Select(f => f.BookId).ToHashSet();
            var toRead = _context.LikeToReadConnector.Where(fc => fc.UserId == userId && bookList.Any(b => b.Id == fc.BookId)).Select(f => f.BookId).ToHashSet();
            var result = new List<BookModel>();
            bookList.ForEach(b => result.Add(
            new BookModel
            {
                Id = b.Id,
                AuthorsNames = b.Authors.Select(a => a.Name).ToList(),
                Description = (b.Description == null || b.Description.Length < 200) ? b.Description : b.Description.Substring(0, 200),
                Rateing = b.ReviewScore,
                Tags = b.TagConnectors.Select(tc => tc.Tag.Label).ToList(),
                Title = b.Title,

                Favourite = favs.Contains(b.Id),
                LikeToRead = toRead.Contains(b.Id),
                Read = read.Contains(b.Id),
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
        public void AddToFavourites(string userId, long bookId)
        {
            if (_context.FavouritesConnector.Any(fc => fc.UserId == userId && fc.BookId == bookId)) return;
            _context.FavouritesConnector.Add(new ConnectionTables.FavouritesConnector { BookId = bookId, UserId = userId });
            _context.SaveChanges();
        }
        public void AddToRead(string userId, long bookId)
        {
            if (_context.ReadConnector.Any(fc => fc.UserId == userId && fc.BookId == bookId)) return;
            _context.ReadConnector.Add(new ConnectionTables.ReadConnector { BookId = bookId, UserId = userId });
            _context.SaveChanges();
        }
        public void AddToLikeToRead(string userId, long bookId)
        {
            if (_context.LikeToReadConnector.Any(fc => fc.UserId == userId && fc.BookId == bookId)) return;
            _context.LikeToReadConnector.Add(new ConnectionTables.LikeToReadConnector { BookId = bookId, UserId = userId });
            _context.SaveChanges();
        }
        public void RemoveFromFavourites(string userId, long bookId)
        {
            if (!_context.FavouritesConnector.Any(fc => fc.UserId == userId && fc.BookId == bookId)) return;
            _context.FavouritesConnector.Remove(new ConnectionTables.FavouritesConnector { BookId = bookId, UserId = userId });
            _context.SaveChanges();
        }

        public void RemoveFromLikeToRead(string userId, long bookId)
        {
            if (!_context.LikeToReadConnector.Any(fc => fc.UserId == userId && fc.BookId == bookId)) return;
            _context.LikeToReadConnector.Remove(new ConnectionTables.LikeToReadConnector { BookId = bookId, UserId = userId });
            _context.SaveChanges();
        }
        public void RemoveFromRead(string userId, long bookId)
        {
            if (!_context.ReadConnector.Any(fc => fc.UserId == userId && fc.BookId == bookId)) return;
            _context.ReadConnector.Remove(new ConnectionTables.ReadConnector { BookId = bookId, UserId = userId });
            _context.SaveChanges();
        }


        public long MakeNewBookProposal(string userId, string title, string description, List<Author> authors, List<string> tags, int? datePublished)
        {
            authors.ForEach(a => a.Id = 0);


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
                Authors = authors,
                Proposer = _context.Users.Find(userId),
                Tags = tagsToAdd,
                DatePublished = datePublished
            };
            _context.BookProposals.Add(bookProposalToAdd);
            _context.SaveChanges();
            return bookProposalToAdd.Id;

        }

        BookModel IBookRepository.Find(long id)
        {
            var book = _context.Books.Include(b => b.TagConnectors).ThenInclude(tc => tc.Book).Where(b => b.Id == id).FirstOrDefault();

            return new BookModel()
            {
                Cover = book.CoverId,
                DatePublished = book.DatePublished,
                Id = book.Id,
                AuthorsNames = book.Authors.Select(a => a.Name).ToList(),
                Description = book.Description,
                Rateing = book.ReviewScore,
                Tags = book.TagConnectors.Select(tc => tc.Tag.Label).ToList(),
                Title = book.Title
            };
        }

        public long AddNewReview(long bookId, ReviewModel review)
        {
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
