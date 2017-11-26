using System;
using System.Collections.Generic;
using System.Text;

namespace CritiquesShelfBLL.RepositoryInterfaces
{
    public interface ITagRepository
    {
        List<string> GetTags();
        void addTag(string label);
    }
}
