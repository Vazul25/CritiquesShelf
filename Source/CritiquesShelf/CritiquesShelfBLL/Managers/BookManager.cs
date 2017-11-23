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

        public PagedData<List<BookModel>> GetBooks(int page, int pageSize,List<string> Tags,string searchText )
        {
 


            IQueryable<Book> query;

            if (pageSize == 0) query = _context.Books.Include(b => b.Authors).Include(b => b.TagConnectors).ThenInclude(tc => tc.Tag).OrderBy(b => b.Id);
            else query = _context.Books.Include(b => b.Authors).Include(b => b.TagConnectors).ThenInclude(tc => tc.Tag).OrderBy(b => b.Id).Skip(pageSize * page).Take(pageSize);
            //var teszt10 = query.ToList();
            if (!searchText.IsNullOrEmpty())
            {
                query = query.Where(b=>b.Title.Contains(searchText) || b.Authors.Any(a=>a.Name.Contains(searchText)));
            }
            if (!Tags.IsNullOrEmpty()) {
                query = query.Where(b => Tags.All(t=>b.TagConnectors.Any(bt=>bt.Tag.Label==t))) ;
            }
          
            var result= new List<BookModel>();
            query.ToList().ForEach(b => result.Add(
            new BookModel
            {
                AuthorsNames = b.Authors.Select(a => a.Name).ToList(),
                Description = b.Description == null || b.Description.Length < 200 ? b.Description : b.Description.Substring(200),
                Rateing = b.ReviewScore,
                Tags = b.TagConnectors.Select(tc => tc.Tag.Label).ToList(),
                Title = b.Title,
                CoverSource = b.CoverId
            }));
            
          
           
            return new PagedData<List<BookModel>>()
            {
                Page = page,
                PageSize = pageSize,
                Data = result,
                HasNext = result.Count == pageSize ? true : false
            };




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
    }
}
