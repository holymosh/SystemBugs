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
                @"Server=.\SQLExpress;Database=myDataBase;Integrated Security=True;Asynchronous Processing=True;";
            serviceCollection.AddEntityFrameworkSqlServer().AddDbContext<DomainDbContext>(builder => builder.UseSqlServer(connectionString));

            serviceCollection.AddScoped(typeof(IGenericRepository<>),typeof(EfGenericRepository<>));
            

        }
    }
}
