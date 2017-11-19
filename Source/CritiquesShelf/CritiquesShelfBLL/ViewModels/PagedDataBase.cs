using System;
using System.Collections.Generic;
using System.Text;

namespace CritiquesShelfBLL.ViewModels
{
    public class PagedData<T>
    {
        public T Data { get; set; }
        public int PageSize { get; set; }
        public bool HasNext { get; set; }
        public int Page { get; set; }

    }
}
