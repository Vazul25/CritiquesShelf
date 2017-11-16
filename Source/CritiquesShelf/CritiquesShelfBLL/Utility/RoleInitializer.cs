using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CritiquesShelfBLL.Utility
{
    public static class RoleInitializer
    {
        //
        private static readonly string[] roles = new[] {
        CritiquesShelfRoles.User.GetName(),
        CritiquesShelfRoles.Admin.GetName()
    };

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            
            foreach (var role in roles)
            {

                if (!await roleManager.RoleExistsAsync(role))
                {
                    var create = await roleManager.CreateAsync(new IdentityRole(role));

                    if (!create.Succeeded)
                    {

                        throw new Exception("Failed to create role");

                    }
                }

            }

        }
    }
}
