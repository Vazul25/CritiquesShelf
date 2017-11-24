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
        private readonly UserManager<ApplicationUser> _identityUserManager;

        public UserApiController(IUserRepository userRepository, UserManager<ApplicationUser> identityUserManager)
        {
            _userRepository = userRepository;
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
            var userBooks = _userRepository.GetUserBooks(id);

            return Ok(userBooks);
        }

    }
}
