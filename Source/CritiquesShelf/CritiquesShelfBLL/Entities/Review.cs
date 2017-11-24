using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace CritiquesShelfBLL.Entities
{
    public class Review : PersistentEntity
    {
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public double Score { get; set; }

        public long BookId { get; set; }

        public Book Book { get; set; }
    }
}
