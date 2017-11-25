using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using CritiquesShelfBLL.ConnectionTables;
using System.Linq;

namespace CritiquesShelfBLL.Entities
{
    public class Book : MetaBook
    {

        public List<Review> Reviews { get; set; }

        public HashSet<TagConnector> TagConnectors { get; set; }

        public HashSet<FavouritesConnector> FavouriteConnectors { get; set; }

        public HashSet<LikeToReadConnector> LikeToReadConnectors { get; set; }

        public HashSet<ReadConnector> ReadConnectors { get; set; }

		public double ReviewScore { get; set; }
    }
}
