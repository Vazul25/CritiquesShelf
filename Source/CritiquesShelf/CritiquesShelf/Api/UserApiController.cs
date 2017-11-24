using System;
using Microsoft.AspNetCore.Mvc;
using CritiquesShelfBLL.Managers;
using CritiquesShelfBLL.Entities;
using CritiquesShelfBLL.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using CritiquesShelfBLL.Utility;
using CritiquesShelfBLL.ViewModels;
using System.Reflection.Metadata;

namespace CritiquesShelf.Api
{
    [Route("api/User")]
    public class UserApiController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        private readonly UserManager<ApplicationUser> _identityUserManager;

        public UserApiController(IUserRepository userRepository, IBookRepository bookRepository, UserManager<ApplicationUser> identityUserManager)
        {
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _identityUserManager = identityUserManager;
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserModel user) {
            var savedUser = _userRepository.Save(user);
            return Ok(savedUser);
        }

        [HttpGet]
        [Route("Current")]
        public async Task<IActionResult> Current() 
        {
            var user = await _identityUserManager.GetUserAsync(HttpContext.User);
            var userModel = _userRepository.Find(user.Id);

            return Ok(userModel);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id) 
        {
            var user = _userRepository.Find(id);
            return Ok(user);
        }
        [HttpGet("Role")]
        public IActionResult GetCurrentUsersRole()
        {
            var userid = _identityUserManager.GetUserId(HttpContext.User);
            return Ok(new {Role= _userRepository.GetRole(userid).GetName() } );
        }

        [HttpGet("{id}/books")]
        public IActionResult GetUserBooks(string id) {
            var userBooks = _bookRepository.GetUserBooks(id);

            return Ok(userBooks);
        }

        [HttpGet("{id}/{collection}")]
        public IActionResult GetPagedUserBooksByCollection(string id, string collection, [FromQuery]int page, [FromQuery]int pageSize) {
            var books = _bookRepository.GetPagedUserBooksByCollection(id, collection, page, pageSize);
            return Ok(books);
        }
    }
}
