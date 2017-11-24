using CritiquesShelfBLL.RepositoryInterfaces;
using CritiquesShelfBLL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CritiquesShelfBLL.Utility;
using System;
using CritiquesShelfBLL.ViewModels;
using CritiquesShelfBLL.Mapper;

namespace CritiquesShelfBLL.Managers
{
    public class UserManager : RepositoryBase<ApplicationUser>, IUserRepository
    {
        private readonly ImageStore _imageStore;
        private readonly IMapper _mapper;

        public UserManager(CritiquesShelfDbContext context, ImageStore imageStore, IMapper mapper) : base(context)
        {
            _imageStore = imageStore;
            _mapper = mapper;
        }

        public UserModel Find(string id) 
        {
            var user = _context.Users
                           .Include(x => x.LikeToRead)
                           .Include(x => x.Favourites)
                           .Include(x => x.Read)
                           .Include(x => x.Reviews).ThenInclude(y => y.Book)
                           .Where(x => x.Id == id)
                           .FirstOrDefault();

            if (user.PhotoId != null) {
				user.Photo = _imageStore.GetImage(user.PhotoId);
            }

            return _mapper.MapUserEntityToModel(user);
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

        public UserModel Save(UserModel user) 
        {
            var entity = _context.Users.Find(user.Id);

            entity.FirstName = user.FirstName;
            entity.LastName = user.LastName;
            entity.Email = user.Email;
            entity.UserName = user.UserName;

            if (user.Photo != null) 
            {
                var photoId = _imageStore.SaveImage(user.Photo);
                entity.PhotoId = photoId;
            }

            _context.SaveChanges();

            return user;
        }
    }
}
