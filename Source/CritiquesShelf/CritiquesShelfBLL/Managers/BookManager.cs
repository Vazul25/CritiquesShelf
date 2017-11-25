using CritiquesShelfBLL.RepositoryInterfaces;
using CritiquesShelfBLL.Entities;
using CritiquesShelfBLL.ViewModels;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CritiquesShelfBLL.Utility;
using CritiquesShelfBLL.Mapper;
using CritiquesShelfBLL.ConnectionTables;

namespace CritiquesShelfBLL.Managers
{
    public class BookManager : RepositoryBase<Book>, IBookRepository
    {
        private readonly IMapper _mapper;

        public BookManager(CritiquesShelfDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public List<Author> GetAuthors()
        {
            return _context.Authors.Select(a => a.Name).Distinct().ToList().Select(a => new Author { Id = 0, Name = a }).ToList();
        }

        public PagedData<List<BookProposalModel>> GetBookProposals(int page, int pageSize)
        {
            IQueryable<BookProposal> query;

            if (pageSize == 0) query = _context.BookProposals.Include(b => b.Proposer).Include(b => b.Tags).OrderBy(b => b.Id);
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

        public PagedData<List<BookModel>> GetBooks(string userId, int page, int pageSize, List<string> Tags, string searchText, string orderBy)
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

            switch (orderBy)
            {
                case "Title":
                    query = query.OrderBy(o => o.Title);
                    break;
                case "Rateing":
                    query = query.OrderBy(o => o.ReviewScore);
                    break;
                case "Date":
                    query = query.OrderBy(o => o.DatePublished);
                    break;
                default:
                    query = query.OrderBy(o => o.Id);

                    break;
            }

            if (pageSize != 0) query = query.Skip(pageSize * page).Take(pageSize);
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
        public void ApproveBookProposal(long id)
        {
            var proposalToAccept = _context.BookProposals.Include(bp => bp.Authors).Include(bp => bp.Tags).First(b => b.Id == id);


            var newBook = new Book
            {
                DatePublished = proposalToAccept.DatePublished,
                Authors = proposalToAccept.Authors.Select(a => new Author { Name = a.Name }).ToList(),
                Description = proposalToAccept.Description,
                Title = proposalToAccept.Title,
                 

            };
            var tagConnectors = _context.Tags.Where(tc => proposalToAccept.Tags.Select(t => t.Label).Contains(tc.Label)).ToList().Select(t=>new TagConnector {Book=newBook,TagId=t.Id }).ToHashSet();
            newBook.TagConnectors = tagConnectors;
            _context.Books.Add(newBook);
            removeProposal(proposalToAccept);
            _context.SaveChanges();
        }
        private void removeProposal(BookProposal proposalToRemove) {
            if(proposalToRemove.Authors!=null) _context.Authors.RemoveRange(proposalToRemove.Authors);
            _context.BookProposals.Remove(proposalToRemove);
            if (proposalToRemove.Tags != null) _context.TagProposals.RemoveRange(proposalToRemove.Tags);
        }
        public void RejectBookProposal(long id)
        {
            var proposalToRemove = _context.BookProposals.Include(bp=>bp.Authors).Include(bp=>bp.Tags).First(b=>b.Id==id);
            removeProposal(proposalToRemove);
            _context.SaveChanges();
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
            var book = _context.Books.Include(x => x.Reviews).First(x => x.Id == bookId);

            var reviewEntity = new Review
            {
                BookId = bookId,
                Score = review.Score,
                Description = review.Description,
                UserId = review.UserId,
                Date = review.Date
            };

            book.Reviews.Add(reviewEntity);
            book.ReviewScore = book.Reviews.Average(x => x.Score);
            _context.SaveChanges();

            return reviewEntity.Id;
        }

        public UserBooksModel GetUserBooks(string userId)
        {

            var reviewedBookQuery = _context.Books
                                           .Where(x => x.Reviews.Any(y => y.UserId == userId))
                                           .Include(x => x.Authors)
                                           .Include(x => x.TagConnectors)
                                           .ThenInclude(x => x.Tag)
                                           .OrderBy(x => x.Title);


            var favouriteBookQuery = _context.Books
                                                .Where(x => x.FavouriteConnectors.Any(y => y.UserId == userId))
                                                .Include(x => x.Authors)
                                                .Include(x => x.TagConnectors)
                                                .ThenInclude(x => x.Tag)
                                                .OrderBy(x => x.Title);

            var likeToReadBookQuery = _context.Books
                                                 .Where(x => x.LikeToReadConnectors.Any(y => y.UserId == userId))
                                                 .Include(x => x.Authors)
                                                 .Include(x => x.TagConnectors)
                                                 .ThenInclude(x => x.Tag)
                                                 .OrderBy(x => x.Title);

            var readBookQuery = _context.Books
                                           .Where(x => x.ReadConnectors.Any(y => y.UserId == userId))
                                           .Include(x => x.Authors)
                                           .Include(x => x.TagConnectors)
                                           .ThenInclude(x => x.Tag)
                                           .OrderBy(x => x.Title);

            return new UserBooksModel
            {
                MaxFavouritesCount = favouriteBookQuery.Count(),
                Favourites = favouriteBookQuery.Take(10).ToList().Select(x => _mapper.MapBookEntityToModel(x)).ToList(),

                MaxLikeToReadCount = likeToReadBookQuery.Count(),
                LikeToRead = likeToReadBookQuery.Take(10).ToList().Select(x => _mapper.MapBookEntityToModel(x)).ToList(),

                MaxReadCount = readBookQuery.Count(),
                Read = readBookQuery.Take(10).ToList().Select(x => _mapper.MapBookEntityToModel(x)).ToList(),

                MaxReviewedCount = reviewedBookQuery.Count(),
                Reviewed = reviewedBookQuery.Take(10).ToList().Select(x => _mapper.MapBookEntityToModel(x)).ToList()
            };
        }

        public List<BookModel> GetPagedUserBooksByCollection(string id, string collection, int page, int pageSize)
        {
            IQueryable<Book> query = _context.Books.Where(x => false);

            switch (collection)
            {
                case "favourites":
                    query = _context.Books
                                    .Where(x => x.FavouriteConnectors.Any(y => y.UserId == id));
                    break;
                case "reviewed":
                    query = _context.Books
                                    .Where(x => x.Reviews.Any(y => y.UserId == id));
                    break;
                case "likeToRead":
                    query = _context.Books
                                    .Where(x => x.LikeToReadConnectors.Any(y => y.UserId == id));
                    break;
                case "read":
                    query = _context.Books
                                    .Where(x => x.ReadConnectors.Any(y => y.UserId == id));
                    break;
            }

            var entities = query.Include(x => x.Authors)
                                .Include(x => x.TagConnectors)
                                .ThenInclude(x => x.Tag)
                                .OrderBy(x => x.Title)
                                .Skip((page) * pageSize)
                                .Take(pageSize)
                                .ToList();

            return entities.Select(x => _mapper.MapBookEntityToModel(x)).ToList();
        }
    }
}
