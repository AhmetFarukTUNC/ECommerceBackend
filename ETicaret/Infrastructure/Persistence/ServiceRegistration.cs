﻿using Application.Abstraction;
using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Concrete;
using Persistence.Contexts;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class ServiceRegistration
    {

        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddSingleton<IProductService,ProductService>();

            


            services.AddDbContext<ETicaretDbContext>(options => options.UseSqlServer(Configuration.ConnectionString),ServiceLifetime.Singleton);


            services.AddSingleton<ICustomerReadRepository,CustomerReadRepository>();

            services.AddSingleton<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddSingleton<IProductReadRepository, ProductReadRepository>();
            services.AddSingleton<IProductWriteRepository,ProductWriteRepository>();
            services.AddSingleton<IOrderReadRepository, OrderReadRepository>();
            services.AddSingleton<IOrderWriteRepository, OrderWriteRepository>();

        }

    }
}
