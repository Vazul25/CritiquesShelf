using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace CritiquesShelfBLL.Entities
{
    public class Book : MetaBook
    {

        public List<Review> Reviews { get; set; }

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
