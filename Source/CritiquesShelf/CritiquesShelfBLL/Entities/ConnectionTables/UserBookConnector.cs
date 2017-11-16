using System;
using System.ComponentModel.DataAnnotations.Schema;
using CritiquesShelfBLL.Entities;
namespace CritiquesShelfBLL.ConnectionTables
{
    public abstract class UserBookConnector
    {
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public long BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}
