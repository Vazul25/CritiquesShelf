using CritiquesShelfBLL.RepositoryInterfaces;
using CritiquesShelfBLL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using System.Linq;
using CritiquesShelfBLL.ConnectionTables;
using CritiquesShelfBLL.Utility;

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

            user.PhotoId = "img.png";
            _context.SaveChanges();
            if (user.PhotoId != null) {
				user.Photo = _imageStore.GetImage(user.PhotoId);
            }

            return user;
        }
    }
}
