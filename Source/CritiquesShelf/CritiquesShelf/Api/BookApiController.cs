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
        public IActionResult Get(  int page=0, int pageSize=0)
        {
            return Ok(_bookManager.GetBooks(page,pageSize));
        }
        [Route("getBooks")]
        [HttpPost]
        public IActionResult GetFiltered([FromBody] GetFilteredRequest request)
        {
            
            return Ok(_bookManager.GetBooks(request.Page, request.PageSize, request.Tags, request.SearchText));
        }

        [Route("postBookProposal")]
        [HttpPost]
        public IActionResult PostBookProposal([FromBody] PostBookProposalModel bookProposal)
        {
            var userId = _identityUserManager.GetUserId(HttpContext.User);
            return Ok(_bookManager.MakeNewBookProposal(userId,bookProposal.Title,bookProposal.Description, bookProposal.Authors,bookProposal.Tags,bookProposal.datePublished));
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
        public BookModel Get(long id)
        {
            return _bookManager.Find(id);
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

        [HttpPost("{id}/review")]
        public IActionResult Post(long id, [FromBody] ReviewModel review) {
            var reviewId = _bookManager.AddNewReview(id, review);
            return Ok(id);
        }
    }
}
