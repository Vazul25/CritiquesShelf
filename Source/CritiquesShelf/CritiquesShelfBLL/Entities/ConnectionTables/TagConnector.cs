using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CritiquesShelfBLL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace CritiquesShelfBLL.ConnectionTables
{
    public class TagConnector
    {
        public long BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        public long TagId { get; set; }

        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
    }
}
