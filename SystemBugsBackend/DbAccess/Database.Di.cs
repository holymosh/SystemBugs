using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DbAccess
{
    public static class DatabaseDI
    {
        public static void RegisterDb(this IServiceCollection serviceCollection)
        {
            var connectionString =
                @"Server=.\SQLExpress;Database=SystemBugs;Integrated Security=True;";
            serviceCollection.AddEntityFrameworkSqlServer().AddDbContext<DomainDbContext>(builder => builder.UseSqlServer(connectionString));

            serviceCollection.AddScoped(typeof(IRepository<>),typeof(EfRepository<>));
            

        }
    }
}
