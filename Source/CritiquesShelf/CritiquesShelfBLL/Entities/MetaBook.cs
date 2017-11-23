using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CritiquesShelfBLL.ConnectionTables;
using System.Linq;

namespace CritiquesShelfBLL.Entities
{
    public abstract class MetaBook : PersistentEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int? DatePublished { get; set; }

        public List<Author> Authors { get; set; }

        public string CoverId { get; set; }

    }
}
