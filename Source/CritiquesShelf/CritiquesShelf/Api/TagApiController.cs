using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CritiquesShelfBLL.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using CritiquesShelfBLL.Entities;
using CritiquesShelfBLL.Utility;

namespace CritiquesShelf.Api
{
    [Route("api/Tag")]
    public class TagApiController : Controller
    {
        private readonly UserManager<ApplicationUser> _identityUserManager;
        ITagRepository _tagManager;
        public TagApiController(  ITagRepository tagManager, UserManager<ApplicationUser>  identityUserManager)
        {
            _identityUserManager = identityUserManager;
            _tagManager = tagManager;
        }
        [Route("getTags")]
        public IActionResult GetTags()
        {

            return Ok(_tagManager.GetTags());
        }
        [Route("addTag/")]
        public async Task<IActionResult> PostTagAsync([FromQuery]string label)
        {
            var user = await _identityUserManager.GetUserAsync(HttpContext.User);
            var roles = await _identityUserManager.GetRolesAsync(user);
            if (roles.Contains(CritiquesShelfRoles.Admin.GetName()))
            {
                _tagManager.addTag(label);
                return Ok(new { message = "ok" });
            }
            else return Unauthorized();
            
        }
    }
}