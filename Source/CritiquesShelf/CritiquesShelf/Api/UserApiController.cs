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
using CritiquesShelf.Controllers;

namespace CritiquesShelf.Api
{
    [Route("api/User")]
    public class UserApiController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        private readonly UserManager<ApplicationUser> _identityUserManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserApiController(IUserRepository userRepository, IBookRepository bookRepository, UserManager<ApplicationUser> identityUserManager, SignInManager<ApplicationUser> signInManager)
        {
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _identityUserManager = identityUserManager;
            _signInManager = signInManager;
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

        [HttpGet("{id}/books/{collection}")]
        public IActionResult GetPagedUserBooksByCollection(string id, string collection, [FromQuery]Paging paging) {
            var books = _bookRepository.GetPagedUserBooksByCollection(id, collection, paging.Page, paging.PageSize);
            return Ok(books);
        }

        [HttpGet("{id}/reviews")]
        public IActionResult GetPagedUserReviews(string id, [FromQuery]Paging paging)
        {
            var reviews = _userRepository.GetPagedUserReviews(id, paging.Page, paging.PageSize);
            return Ok(reviews);
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
