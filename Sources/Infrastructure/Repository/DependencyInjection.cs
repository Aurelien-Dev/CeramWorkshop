﻿using Domain.Interfaces;
using Domain.InterfacesWorker;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using Repository.Workers;

namespace Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            //Repositories
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IMaterialRepository, MaterialRepository>();
            services.AddTransient<IFiringRepository, FiringRepository>();
            services.AddTransient<IWorkshopRepository, WorkshopRepository>();
            services.AddTransient<IImageInstructionRepository, ImageInstructionRepository>();

            //Workers
            services.AddTransient<IProductWorker, ProductWorker>();
            services.AddTransient<IWorkshopWorker, WorkshopWorker>();
            services.AddTransient<IApiWorker, ApiWorker>();

            //DbContext
            services.AddDbContext<ApplicationDbContext>();

            return services;
        }
    }
}