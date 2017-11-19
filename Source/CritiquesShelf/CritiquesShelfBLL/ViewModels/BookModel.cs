using System;
using System.Collections.Generic;
using System.Text;

namespace CritiquesShelfBLL.ViewModels
{
    class BookModel
    {
        public string Title{ get; set; }
        public string Description { get; set; }
        public List<string> AuthorsNames { get; set; }
        public double Rateing { get; set; }
        public List<string> Tags { get; set; }
    }
}
