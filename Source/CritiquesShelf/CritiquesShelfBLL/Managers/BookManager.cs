using CritiquesShelfBLL.RepositoryInterfaces;
using CritiquesShelfBLL.Entities;
using CritiquesShelfBLL.ViewModels;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

            if (pageSize == 0) query = _context.BookProposals.Include(b=>b.Proposer).Include(b => b.Tags).OrderBy(b => b.Id);
            else if (page == 0) query = _context.BookProposals.Include(b => b.Proposer).Include(b => b.Tags).OrderBy(b => b.Id).Take(pageSize);
            else query = _context.BookProposals.Include(b => b.Proposer).Include(b => b.Tags).OrderBy(b => b.Id).Skip(pageSize * page).Take(pageSize);
            var data = query.Select(b => new BookProposalModel
            {
                AuthorsNames = b.Authors.Select(a => a.Name).ToList(),
                Description = b.Description,
                Id=b.Id,
                ProposerName=b.Proposer.UserName,
                Tags = b.Tags.Select(t=>t.Label).ToList(),
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

        public PagedData<List<BookModel>> GetBooks(int page, int pageSize)
        {

            #region Test
            /*
            if (pageSize == 0)
            {
                var data = _context.Books.Include(b => b.TagConnectors).ThenInclude(tc => tc.Book).OrderBy(b => b.Id).Select(b => new BookModel
                {
                    AuthorsNames = b.Authors.Select(a => a.Name).ToList(),
                    Description = b.Description,
                    Rateing = b.ReviewScore,
                    Tags = b.TagConnectors.Select(tc => tc.Tag.Label).ToList(),
                    Title = b.Title
                })?.ToList();
                return new PagedData<List<BookModel>>()
                {
                    Page = page,
                    PageSize = pageSize,
                    Data = data,
                    HasNext = data.Count == pageSize ? true : false
                };
            }

            else if (page == 0)
            {
                var data = _context.Books.Include(b => b.TagConnectors).ThenInclude(tc => tc.Book).OrderBy(b => b.Id).Take(pageSize).Select(b => new BookModel
                {
                    AuthorsNames = b.Authors.Select(a => a.Name).ToList(),
                    Description = b.Description,
                    Rateing = b.ReviewScore,
                    Tags = b.TagConnectors.Select(tc => tc.Tag.Label).ToList(),
                    Title = b.Title
                })?.ToList();
                return new PagedData<List<BookModel>>()
                {
                    Page = page,
                    PageSize = pageSize,
                    Data = data,
                    HasNext = data.Count == pageSize ? true : false
                };
            }

            else
            {
                var data = _context.Books.Include(b => b.TagConnectors).ThenInclude(tc => tc.Book).OrderBy(b => b.Id).Skip(pageSize * page).Take(pageSize).Select(b => new BookModel
                {
                    AuthorsNames = b.Authors.Select(a => a.Name).ToList(),
                    Description = b.Description,
                    Rateing = b.ReviewScore,
                    Tags = b.TagConnectors.Select(tc => tc.Tag.Label).ToList(),
                    Title = b.Title
                })?.ToList();
                return new PagedData<List<BookModel>>()
                {
                    Page = page,
                    PageSize = pageSize,
                    Data = data,
                    HasNext = data.Count == pageSize ? true : false
                };
            }*/
            #endregion



            IQueryable<Book> query;

            if (pageSize == 0) query = _context.Books.Include(b => b.TagConnectors).ThenInclude(tc => tc.Book).OrderBy(b => b.Id);
            else if (page == 0) query = _context.Books.Include(b => b.TagConnectors).ThenInclude(tc => tc.Book).OrderBy(b => b.Id).Take(pageSize);
            else query = _context.Books.Include(b => b.TagConnectors).ThenInclude(tc => tc.Book).OrderBy(b => b.Id).Skip(pageSize * page).Take(pageSize);
            var data = query.Select(b => new BookModel
            {
                AuthorsNames = b.Authors.Select(a => a.Name).ToList(),
                Description = b.Description,
                Rateing = b.ReviewScore,
                Tags = b.TagConnectors.Select(tc => tc.Tag.Label).ToList(),
                Title = b.Title
            })?.ToList();
            foreach (var book in data)
            {
                if (book.Description != null && book.Description.Length > 200)
                    book.Description = book.Description.Substring(0, 200) + "...";
            }
            return new PagedData<List<BookModel>>()
            {
                Page = page,
                PageSize = pageSize,
                Data = data,
                HasNext = data.Count == pageSize ? true : false
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
