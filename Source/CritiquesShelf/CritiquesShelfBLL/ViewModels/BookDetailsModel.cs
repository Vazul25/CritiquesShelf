using System;
using System.Collections.Generic;
using System.Text;

namespace CritiquesShelfBLL.ViewModels
{
    public class BookDetailsModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> AuthorsNames { get; set; }
        public double Rateing { get; set; }
        public List<string> Tags { get; set; }
        public int? DatePublished { get; set; }

        
        public int FavouriteCount { get; set; }
        public string Cover { get; set; }
        public List<ReviewModel> Reviews { get; set; }
    }
}
