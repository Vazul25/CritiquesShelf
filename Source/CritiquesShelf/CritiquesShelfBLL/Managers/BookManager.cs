using CritiquesShelfBLL.RepositoryInterfaces;

namespace CritiquesShelfBLL.Managers
{
    public class BookManager : RepositoryBase, IBookRepository
    {
        public BookManager(CritiquesShelfDbContext context): base(context) {
            
        }
    }
}
