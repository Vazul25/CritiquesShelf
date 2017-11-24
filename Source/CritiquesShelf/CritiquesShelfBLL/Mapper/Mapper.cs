using System;
using System.Linq;
using CritiquesShelfBLL.Entities;
using CritiquesShelfBLL.ViewModels;

namespace CritiquesShelfBLL.Mapper
{
    public class Mapper : IMapper
    {
        public BookModel MapBookEntityToModel(Book book)
        {
            if (book == null) {
                return null;
            }

            return new BookModel
            {
                AuthorsNames = book.Authors?.Select(a => a.Name).ToList(),
                Description = (book.Description == null || book.Description.Length < 200) ? book.Description : book.Description.Substring(0, 200),
                Rateing = book.ReviewScore,
                Tags = book.TagConnectors?.Select(tc => tc.Tag.Label).ToList(),
                Title = book.Title,
                Cover = book.CoverId,
                DatePublished = book.DatePublished
            };
        }
    }
}
