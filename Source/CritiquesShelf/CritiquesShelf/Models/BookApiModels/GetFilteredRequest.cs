using System;
using System.Collections.Generic;
namespace CritiquesShelf.BookApiModels
{
    public class GetFilteredRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<string> Tags { get; set; }
        public string SearchText { get; set; }
        public string OrderBy { get; set; }
    }
}
