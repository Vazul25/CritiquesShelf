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
            List<BookModel> GetBooks(int page = 0, int pageSize = 0)
            {
             return   _context.Books.Include(b=>b.TagConnectors).ThenInclude(tc=>tc.Book).Skip(page * pageSize).Take(pageSize).Select(b => new BookModel
                {
                    AuthorsNames = b.Authors.Select(a => a.Name).ToList(),
                    Description = b.Description,
                    Rateing = b.ReviewScore,
                    Tags = b.TagConnectors.Select(tc => tc.Tag.Label).ToList(),
                    Title = b.Title
                }).ToList();
            }
        }

    }
}
