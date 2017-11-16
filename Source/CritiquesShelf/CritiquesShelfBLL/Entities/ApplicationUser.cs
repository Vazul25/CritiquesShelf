using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CritiquesShelfBLL.ConnectionTables;
using Microsoft.AspNetCore.Identity;

namespace CritiquesShelfBLL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }

        public List<FavouritesConnector> Favourites { get; set; }
   
        public List<ReadConnector> Read { get; set; }

        public List<LikeToReadConnector> LikeToRead { get; set; }

        public List<Review> Reviews { get; set; }
    }
}
