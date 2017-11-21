using System;
using System.Collections.Generic;
using System.Text;

namespace CritiquesShelfBLL.ViewModels
{
    public class BookProposalModel   
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> AuthorsNames { get; set; }
        
        public List<string> Tags { get; set; }
        public string ProposerName { get; set; }
    }
}
