using CritiquesShelfBLL.Managers;
using CritiquesShelfBLL.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CritiquesShelfBLL
{
    public static class BLL_DIExtension
    {

        public static IServiceCollection AddBll(this IServiceCollection services, string sqlConnectionString)

        {

            services.AddScoped<IExampleRepository, ExampleManager>();
           
            services.AddDbContext<CritiquesShelfDbContext>(options =>

                        options.UseSqlServer(sqlConnectionString));



            return services;

        }

    }
}
