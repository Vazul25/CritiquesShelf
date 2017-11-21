using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CritiquesShelfBLL.Entities;
using CritiquesShelfBLL.Managers;
using CritiquesShelfBLL.ViewModels;
using CritiquesShelfBLL.RepositoryInterfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CritiquesShelf.Api
{
    [Route("api/Book")]
    public class BookApiController : Controller
    {
        IBookRepository _bookManager;
        public BookApiController(IBookRepository bookManager)
        {
            _bookManager = bookManager;
        }
        // GET: api/values

        [Route("getBooks")]
        [HttpGet]
        public IActionResult Get(  int page=0, int pageSize=0)
        {
            return Ok(_bookManager.GetBooks(page,pageSize));
        }
        [Route("getBookProposals")]
        [HttpGet]
        public IActionResult GetBookProposals(int page = 0, int pageSize = 0)
        {
            return Ok(_bookManager.GetBookProposals(page, pageSize));
        }
      
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
    }
}
