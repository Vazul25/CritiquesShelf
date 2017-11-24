using CritiquesShelfBLL.Entities;
using CritiquesShelfBLL.Utility;
using CritiquesShelfBLL.ViewModels;

namespace CritiquesShelfBLL.RepositoryInterfaces
{
    public interface IUserRepository
    {
        UserModel Find(string id);
        CritiquesShelfRoles GetRole(string userId);
        UserModel Save(UserModel user);
    }
}
