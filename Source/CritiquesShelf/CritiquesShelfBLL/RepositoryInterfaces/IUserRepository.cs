using CritiquesShelfBLL.Entities;

namespace CritiquesShelfBLL.RepositoryInterfaces
{
    public interface IUserRepository
    {
        ApplicationUser Find(string id);
    }
}
