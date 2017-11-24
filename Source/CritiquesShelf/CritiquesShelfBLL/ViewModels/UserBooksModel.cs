using System;
using System.Collections.Generic;

namespace CritiquesShelfBLL.ViewModels
{
    public class UserBooksModel
    {
        public List<BookModel> Favourites { get; set; }
        public int MaxFavouritesCount { get; set; }
        public List<BookModel> Reviewed { get; set; }
        public int MaxReviewedCount { get; set; }
        public List<BookModel> Read { get; set; }
        public int MaxReadCount { get; set; }
        public List<BookModel> LikeToRead { get; set; }
        public int MaxLikeToReadCount { get; set; }
    }
}