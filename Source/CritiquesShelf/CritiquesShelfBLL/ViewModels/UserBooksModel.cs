using System;
using System.Collections.Generic;

namespace CritiquesShelfBLL.ViewModels
{
    public class UserBooksModel
    {
        public List<BookModel> Favourites { get; set; }
        public List<BookModel> Reviewed { get; set; }
        public List<BookModel> Read { get; set; }
        public List<BookModel> LikeToRead { get; set; }
    }
}