using Domain.Interfaces;
using Domain.InterfacesWorker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
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
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var cs = $"Host=192.168.1.19;Username=postgres;Password={Environment.GetEnvironmentVariable("PG_PASSWD")};Database={Environment.GetEnvironmentVariable("PG_DB_NAME")}";
                options.UseNpgsql(new NpgsqlConnection(cs));
            });

            return services;
        }
    }
}