using CritiquesShelfBLL.RepositoryInterfaces;
using CritiquesShelfBLL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using System.Linq;
using CritiquesShelfBLL.ConnectionTables;
using CritiquesShelfBLL.Utility;
using System;

namespace CritiquesShelfBLL.Managers
{
    public class UserManager : RepositoryBase<ApplicationUser>, IUserRepository
    {
        private readonly ImageStore _imageStore;

        public UserManager(CritiquesShelfDbContext context, ImageStore imageStore) : base(context)
        {
            _imageStore = imageStore;
        }

        public ApplicationUser Find(string id) 
        {
            var user = _context.Users
                           .Include(x => x.LikeToRead)
                           .Include(x => x.Favourites)
                           .Include(x => x.Read)
                           .Include(x => x.Reviews)
                           .Where(x => x.Id == id)
                           .FirstOrDefault();

            if (user.PhotoId != null) {
				user.Photo = _imageStore.GetImage(user.PhotoId);
            }

            return user;
        }

        public CritiquesShelfRoles GetRole(string userId)
        {
            var roleName=   _context.Roles.First(r => r.Id == _context.UserRoles.First(ur => ur.UserId == userId).RoleId).Name;
            foreach(CritiquesShelfRoles enumValue in Enum.GetValues(typeof(CritiquesShelfRoles))  )
            {
                if(enumValue.GetName() == roleName) return enumValue;
            }
            throw new Exception($"There was no CritiquesShelfRoles enum defined to the roleName in the database: {roleName}");
        }
    }
}
