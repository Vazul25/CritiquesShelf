using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CritiquesShelfBLL.Entities;
using CritiquesShelfBLL.Managers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CritiquesShelf.Api
{
    [Route("api/Book")]
    public class BookApiController : Controller
    {
        BookManager _bookManager;
        public BookApiController(BookManager bookManager)
        {
            _bookManager = bookManager;
        }
        // GET: api/values
        [HttpGet]
        public List<Book> Get()
        {
            return _bookManager.List();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Book Get(long id)
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
