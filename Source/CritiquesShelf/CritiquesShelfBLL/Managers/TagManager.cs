using CritiquesShelfBLL.Entities;
using CritiquesShelfBLL.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CritiquesShelfBLL.Managers
{
   public class TagManager : RepositoryBase<Tag>,ITagRepository
    {
        public TagManager(CritiquesShelfDbContext context) : base(context)
        {

        }
        public List<string> GetTags()
        {
           return  _context.Tags.Select(t => t.Label).ToList();

        }
    }
}
