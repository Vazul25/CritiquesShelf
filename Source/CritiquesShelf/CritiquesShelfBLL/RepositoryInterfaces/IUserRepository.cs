using CritiquesShelfBLL.Entities;
using CritiquesShelfBLL.Utility;

namespace CritiquesShelfBLL.RepositoryInterfaces
{
    public interface IUserRepository
    {
        ApplicationUser Find(string id);
        CritiquesShelfRoles GetRole(string userId);
    }
}
