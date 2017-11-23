using CritiquesShelfBLL.Entities;
using System.Collections.Generic;

namespace CritiquesShelf.Api
{
    public class PostBookProposalModel
    {
        public List<Author> Authors{ get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Tags{ get; set; }
        public int? datePublished { get; set; }
    }
}