using Application.Services;
using Infra.EF.Interfaces;
using Infra.EF.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection ConfigurationService(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IAddressService, AddressService>();
            return services;
        }
        public static IServiceCollection ConfigurationRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            return services;
        }
        public static IServiceCollection ConfigurationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            // var connection = configuration.GetConnectionString("DefaultConnection");
            // services.AddDbContext<AppDataContext>(options => options.UseSqlite(connection));
            return services;
        }
    }
}