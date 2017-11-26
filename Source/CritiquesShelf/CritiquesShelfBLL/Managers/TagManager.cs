using CritiquesShelfBLL.Entities;
using CritiquesShelfBLL.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CritiquesShelfBLL.Managers
{
    public class TagManager : RepositoryBase<Tag>, ITagRepository
    {
        public TagManager(CritiquesShelfDbContext context) : base(context)
        {

        }

        public void addTag(string label)
        {

            if (!_context.Tags.Any(t => t.Label == label))
            {
                _context.Tags.Add(new Tag { Label = label }); _context.SaveChanges();
            };
            return;

        }

        public List<string> GetTags()
        {
            return _context.Tags.Select(t => t.Label).ToList();

        }
    }
}
