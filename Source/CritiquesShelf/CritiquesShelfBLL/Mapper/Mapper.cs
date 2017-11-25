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

        public ReviewModel MapReviewEntityToModel(Review review)
        {
            return new ReviewModel
            {
                Id = review.Id,
                Date = review.Date,
                Description = review.Description,
                Score = review.Score,
                BookId = review.BookId,
                BookTitle = review.Book?.Title,
                UserId = review.UserId,
                UserName = review.User?.UserName
            };
        }

        public UserModel MapUserEntityToModel(ApplicationUser user)
        {
            return new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Photo = user.Photo,
                UserName = user.UserName,
                Reviews = user.Reviews?.Select(x => MapReviewEntityToModel(x)).ToList(),
                ReadingStat = new ReadingStatModel
                {
                    FavouritesCount = user.Favourites?.Count() ?? 0,
                    LikeToReadCount = user.LikeToRead?.Count() ?? 0,
                    ReadCount = user.Read?.Count() ?? 0
                }
            };
        }
        public static Func<Review, ReviewModel> MapReviewToModelExpression() {
            return (review => new ReviewModel
            {
                Id = review.Id,
                Date = review.Date,
                Description = review.Description,
                Score = review.Score,
                BookId = review.BookId,
                BookTitle = review.Book?.Title,
                UserId = review.UserId,
                UserName = review.User?.UserName
            });
        }
    }

}
