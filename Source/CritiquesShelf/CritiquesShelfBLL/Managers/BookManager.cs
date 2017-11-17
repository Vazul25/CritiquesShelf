using CritiquesShelfBLL.RepositoryInterfaces;
using CritiquesShelfBLL.Entities;

namespace CritiquesShelfBLL.Managers
{
    public class BookManager : RepositoryBase<Book>, IBookRepository
    {
        public BookManager(CritiquesShelfDbContext context): base(context) {
            
        }

    }
}
