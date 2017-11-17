using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
namespace CritiquesShelfBLL.Entities
{
    public class BookProposal : MetaBook
    {
        public string ProposerId { get; set; }

        public HashSet<TagProposal> Tags { get; set; }

        [ForeignKey("ProposerId")]
        public ApplicationUser Proposer { get; set; }
    }
}
