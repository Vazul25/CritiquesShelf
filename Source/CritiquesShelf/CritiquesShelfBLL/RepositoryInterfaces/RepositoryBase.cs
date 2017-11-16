using System;
namespace CritiquesShelfBLL.RepositoryInterfaces
{
    public class RepositoryBase
    {
        protected readonly CritiquesShelfDbContext _context;

        public RepositoryBase(CritiquesShelfDbContext context) {
            _context = context;
        }
    }
}
