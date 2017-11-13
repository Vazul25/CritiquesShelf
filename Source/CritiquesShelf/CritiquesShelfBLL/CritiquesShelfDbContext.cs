using CritiquesShelfBLL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CritiquesShelfBLL
{
    public class CritiquesShelfDbContext : IdentityDbContext<ApplicationUser>
    {
        public CritiquesShelfDbContext(DbContextOptions<CritiquesShelfDbContext> options)
            : base(options)
        {


        }
      //  public virtual DbSet<Type1> Name1{ get; set; }
       
         
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
          //  builder.Entity<Type1>().ToTable("TableName1");
         
        }
    }
}
