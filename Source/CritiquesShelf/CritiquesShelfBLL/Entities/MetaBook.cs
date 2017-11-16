using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CritiquesShelfBLL.ConnectionTables;

namespace CritiquesShelfBLL.Entities
{
    public abstract class MetaBook : PersistentEntity
    {
        public string Title { get; set; }

        public string AuthorLastName { get; set; }

        public string AuthorFirstName { get; set; }

        public string Description { get; set; }

        public DateTime DatePublished { get; set; }

        public HashSet<TagConnector> Tags { get; set; }

        #region Computed Properties
        [NotMapped]
        public string Author
        {
            get
            {
                return AuthorLastName + AuthorFirstName;
            }
        }
        #endregion
    }
}
