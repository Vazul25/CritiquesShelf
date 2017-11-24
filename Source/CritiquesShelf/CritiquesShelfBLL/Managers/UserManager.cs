using CritiquesShelfBLL.RepositoryInterfaces;
using CritiquesShelfBLL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using System.Linq;
using CritiquesShelfBLL.ConnectionTables;
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

            var reviews = user.Reviews?.Select(x => new ReviewModel
            {
                Id = x.Id,
                Date = x.Date,
                Description = x.Description,
                Score = x.Score,
                BookId = x.BookId,
                BookTitle = x.Book?.Title,
                UserId = user.Id,
                UserName = user.UserName
            }).ToList();

            return new UserModel {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Photo = user.Photo,
                UserName = user.UserName,
                Reviews = reviews,
                ReadingStat = new ReadingStatModel {
                    FavouritesCount = user.Favourites?.Count() ?? 0,
                    LikeToReadCount = user.LikeToRead?.Count() ?? 0,
                    ReadCount = user.Read?.Count() ?? 0
                }
            };
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

        public UserBooksModel GetUserBooks(string userId) {

            var reviewedBookEntities = _context.Books
                                               .Where(x => x.Reviews.Any(y => y.UserId == userId))
                                               .Include(x => x.Authors)
                                               .Include(x => x.TagConnectors)
                                               .ThenInclude(x => x.Tag)
                                               .ToList();

            var favouriteBookEntities = _context.Books
                                                .Where(x => x.FavouriteConnectors.Any(y => y.UserId == userId))
                                                .Include(x => x.Authors)
                                                .Include(x => x.TagConnectors)
                                                .ThenInclude(x => x.Tag)
                                                .ToList();

            var likeToReadBookEntities = _context.Books
                                                 .Where(x => x.LikeToReadConnectors.Any(y => y.UserId == userId))
                                                 .Include(x => x.Authors)
                                                 .Include(x => x.TagConnectors)
                                                 .ThenInclude(x => x.Tag)
                                                 .ToList();

            var readBookEntities = _context.Books
                                           .Where(x => x.ReadConnectors.Any(y => y.UserId == userId))
                                           .Include(x => x.Authors)
                                           .Include(x => x.TagConnectors)
                                           .ThenInclude(x => x.Tag)
                                           .ToList();

            return new UserBooksModel {
                Favourites = favouriteBookEntities.Select(x => _mapper.MapBookEntityToModel(x)).ToList(),
                LikeToRead = likeToReadBookEntities.Select(x => _mapper.MapBookEntityToModel(x)).ToList(),
                Read = readBookEntities.Select(x => _mapper.MapBookEntityToModel(x)).ToList(),
                Reviewed = reviewedBookEntities.Select(x => _mapper.MapBookEntityToModel(x)).ToList()
            };
        }
    }
}
