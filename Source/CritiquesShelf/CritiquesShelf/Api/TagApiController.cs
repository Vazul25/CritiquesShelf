using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CritiquesShelfBLL.RepositoryInterfaces;

namespace CritiquesShelf.Api
{
    [Route("api/Tag")]
    public class TagApiController : Controller
    {
    
        ITagRepository _tagManager;
        public TagApiController(  ITagRepository tagManager)
        {
            
            _tagManager = tagManager;
        }
        [Route("getTags")]
        public IActionResult GetTags()
        {

            return Ok(_tagManager.GetTags());
        }
    }
}