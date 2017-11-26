using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CritiquesShelfBLL.Entities;
using CritiquesShelfBLL.Managers;
using CritiquesShelfBLL.ViewModels;
using CritiquesShelfBLL.RepositoryInterfaces;
using CritiquesShelf.BookApiModels;
using Microsoft.AspNetCore.Identity;
using CritiquesShelfBLL.Utility;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CritiquesShelf.Api
{
    [Route("api/Book")]
    public class BookApiController : Controller
    {

        private readonly UserManager<ApplicationUser> _identityUserManager;
        private readonly IBookRepository _bookManager;
        private readonly ITagRepository _tagManager;

        public BookApiController(IBookRepository bookManager, ITagRepository tagManager, UserManager<ApplicationUser> identityUserManager)
        {
            _bookManager = bookManager;
            _identityUserManager = identityUserManager;
            _tagManager = tagManager;
        }
        // GET: api/values

        [Route("getBooks")]
        [HttpGet]
        public IActionResult Get(int page = 0, int pageSize = 0)
        {
            var userId = _identityUserManager.GetUserId(HttpContext.User);
            return Ok(_bookManager.GetBooks(userId, page, pageSize));
        }
        [Route("getBooks")]
        [HttpPost]
        public IActionResult GetFiltered([FromBody] GetFilteredRequest request)
        {
            var userId = _identityUserManager.GetUserId(HttpContext.User);
            return Ok(_bookManager.GetBooks(userId, request.Page, request.PageSize, request.Tags, request.SearchText,request.OrderBy));
        }

        [Route("postBookProposal")]
        [HttpPost]
        public IActionResult PostBookProposal([FromBody] PostBookProposalModel bookProposal)
        {
            var userId = _identityUserManager.GetUserId(HttpContext.User);
            return Ok(_bookManager.MakeNewBookProposal(userId, bookProposal.Title, bookProposal.Description, bookProposal.Authors, bookProposal.Tags, bookProposal.datePublished));
        }

        [Route("getBookProposals")]
        [HttpGet]
        public IActionResult GetBookProposals(int page = 0, int pageSize = 0)
        {
            return Ok(_bookManager.GetBookProposals(page, pageSize));
        }
        [Route("getAuthors")]
        [HttpGet]
        public IActionResult GetAuthors(int page = 0, int pageSize = 0)
        {
            return Ok(_bookManager.GetAuthors());
        }
        [Route("getTags")]
        [HttpGet]

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetBookDetails(long id)
        {
            var userId = _identityUserManager.GetUserId(HttpContext.User);
            return Ok( _bookManager.GetBookDetails(userId,id));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        [HttpDelete("removeFromLikeToRead/{bookId}")]
        public IActionResult DeleteFromLikeToRead(long bookId)
        {
            var userId = _identityUserManager.GetUserId(HttpContext.User);
            _bookManager.RemoveFromLikeToRead(userId, bookId);
            return Ok(new { message = "ok" });
        }
        [HttpDelete("removeFromRead/{bookId}")]
        public IActionResult DeleteFromRead(long bookId)
        {
            var userId = _identityUserManager.GetUserId(HttpContext.User);
            _bookManager.RemoveFromRead(userId, bookId);
            return Ok(new { message = "ok" });
        }
        [HttpDelete("removeFromFavourites/{bookId}")]
        public IActionResult DeleteFromFavourite(long bookId)
        {
            var userId = _identityUserManager.GetUserId(HttpContext.User);
            _bookManager.RemoveFromFavourites(userId, bookId);
            return Ok(new { message = "ok" });
        }

        [HttpPost("addToLikeToRead/{bookId}")]
        public IActionResult AddToLikeToRead(long bookId)
        {
            var userId = _identityUserManager.GetUserId(HttpContext.User);
            _bookManager.AddToLikeToRead(userId, bookId);
            return Ok(new { message = "ok" });
        }
        [HttpPost("addToRead/{bookId}")]
        public IActionResult AddToRead(long bookId)
        {
            var userId = _identityUserManager.GetUserId(HttpContext.User);
            _bookManager.AddToRead(userId, bookId);
            return Ok(new { message = "ok" });
        }
        [HttpPost("addToFavourites/{bookId}")]
        public IActionResult AddToFavourites(long bookId)
        {
            var userId = _identityUserManager.GetUserId(HttpContext.User);
            _bookManager.AddToFavourites(userId, bookId);
            return Ok(new { message = "ok" });
        }


        [HttpPost("{id}/review")]
        public IActionResult Post(long id, [FromBody] ReviewModel review)
        {
            var reviewId = _bookManager.AddNewReview(id, review);
            return Ok(id);
        }
        [HttpPut("updateReview")]
        public IActionResult UpdateReview([FromBody] ReviewModel review)
        {
            var userId = _identityUserManager.GetUserId(HttpContext.User);
            if (userId != review.UserId) return Unauthorized();
           _bookManager.UpdateReview(review);
            return Ok(new { message = "Update success" });
        }

        [HttpPut("approveBookProposal/{bookId}")]
        public async Task<IActionResult> ApproveBookProposal(long bookId)
        {
            var user = await _identityUserManager.GetUserAsync(HttpContext.User);
            var roles = await _identityUserManager.GetRolesAsync(user);
            if (roles.Contains(CritiquesShelfRoles.Admin.GetName()))
            {
                _bookManager.ApproveBookProposal(bookId);
                return Ok(new { message = "ok" });
            }
            else return Unauthorized();

        }
        [HttpDelete("rejectBookProposal/{bookId}")]
        public async Task<IActionResult> RejectBookProposal(long bookId)
        {
            var user = await _identityUserManager.GetUserAsync(HttpContext.User);
            var roles = await _identityUserManager.GetRolesAsync(user);
            if (roles.Contains(CritiquesShelfRoles.Admin.GetName()))
            {
                _bookManager.RejectBookProposal(bookId);
                return Ok(new { message = "ok" });
            }
            else return Unauthorized();


        }
        [HttpGet("getBookReviews/{id}")]
        public IActionResult GetPagedUserReviews(long id, [FromQuery]Paging paging)
        {
            var reviews = _bookManager.GetPagedBookReviews(id, paging.Page, paging.PageSize);
            return Ok(reviews);
        }
        [HttpGet("getTrendingBooks")]
        public IActionResult GetTrendingBooks( )
        {
            var reviews = _bookManager.GetTrendingBooks( );
            return Ok(reviews);
        }
        [HttpGet("getTrendingReviews")]
        public IActionResult GetTrendingReviews( )
        {
            var reviews = _bookManager.GetTrendingReviews( );
            return Ok(reviews);
        }


    }
}
