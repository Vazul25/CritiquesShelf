using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace CritiquesShelfBLL.Entities
{
    public class BookProposal : MetaBook
    {
        public string ProposerId { get; set; }

        [ForeignKey("ProposerId")]
        public ApplicationUser Proposer { get; set; }
    }
}
