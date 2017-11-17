﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using CritiquesShelfBLL.ConnectionTables;

namespace CritiquesShelfBLL.Entities
{
    public class Book : MetaBook
    {

        public List<Review> Reviews { get; set; }

        public HashSet<TagConnector> TagConnectors { get; set; }

        #region Computed Properties

        [NotMapped]
        public double ReviewScore
        {
            get
            {
                return 0;
            }
        }
        #endregion
    }
}
