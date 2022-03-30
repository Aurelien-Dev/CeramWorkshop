using Common.Utils.Singletons;
using Domain.Interfaces;
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
            services.AddTransient<IAccessoryRepository, AccessoryRepository>();

            //Workers
            services.AddTransient<IProductWork, ProductWork>();

            //DbContext
            services.AddDbContext<ApplicationDbContext>();
            
            return services;
        }
    }
}