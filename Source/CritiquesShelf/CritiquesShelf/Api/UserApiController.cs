using System;
using Microsoft.AspNetCore.Mvc;
using CritiquesShelfBLL.Managers;
using CritiquesShelfBLL.Entities;
using CritiquesShelfBLL.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using CritiquesShelfBLL.Utility;

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

        [HttpGet]
        [Route("Current")]
        public async Task<ApplicationUser> Current() 
        {
            var user = await _identityUserManager.GetUserAsync(HttpContext.User);
            return _userRepository.Find(user.Id);
        }

        [HttpGet("{id}")]
        public ApplicationUser Get(string id) 
        {
            return _userRepository.Find(id);
        }
        [HttpGet("Role")]
        public IActionResult GetCurrentUsersRole()
        {
            var userid = _identityUserManager.GetUserId(HttpContext.User);
            return Ok(new {Role= _userRepository.GetRole(userid).GetName() } );
        }

    }
}
