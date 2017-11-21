using CritiquesShelfBLL.Managers;
using CritiquesShelfBLL.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using CritiquesShelfBLL.Utility;

namespace CritiquesShelfBLL
{
    public static class BLL_DIExtension
    {

        public static IServiceCollection AddBll(this IServiceCollection services, string sqlConnectionString)

        {

			services.AddDbContext<CritiquesShelfDbContext>(options =>
			                                               options.UseSqlServer(sqlConnectionString));
            
            services.AddScoped<IUserRepository,UserManager>();
            services.AddScoped<ITagRepository, TagManager>();
            services.AddScoped<IReviewRepository, ReviewManager>();
            services.AddScoped<IBookRepository, BookManager>();
            services.AddScoped<ImageStore, ImageStore>();

            return services;

        }

    }
}
