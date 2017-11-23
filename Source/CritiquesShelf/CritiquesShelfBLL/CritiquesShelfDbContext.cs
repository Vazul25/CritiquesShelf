using CritiquesShelfBLL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using CritiquesShelfBLL.ConnectionTables;

namespace CritiquesShelfBLL
{
    public class CritiquesShelfDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Book> Books { get; set; }
		public DbSet<BookProposal> BookProposals { get; set; }
        public DbSet<Author> Authors { get; set;  }
        public DbSet<Review> Reviews { get; set; }
		public DbSet<Tag> Tags { get; set; }
        public DbSet<FavouritesConnector> FavouritesConnector { get; set; }
        public DbSet<LikeToReadConnector> LikeToReadConnector { get; set; }
        public DbSet<ReadConnector> ReadConnector { get; set; }
        public DbSet<TagConnector> TagConnector { get; set; }
        public DbSet<TagProposal> TagProposals{ get; set; }
        public CritiquesShelfDbContext(DbContextOptions<CritiquesShelfDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();   
        }
         
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FavouritesConnector>()
                   .HasKey(x => new { x.BookId, x.UserId });

            builder.Entity<LikeToReadConnector>()
                   .HasKey(x => new { x.BookId, x.UserId });

            builder.Entity<ReadConnector>()
                   .HasKey(x => new { x.BookId, x.UserId });

            builder.Entity<TagConnector>()
                   .HasKey(x => new { x.BookId, x.TagId });

            builder.Entity<Tag>()
                   .HasAlternateKey(x => x.Label);

        }
    }
}
