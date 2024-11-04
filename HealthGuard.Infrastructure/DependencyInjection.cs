using HealthGuard.Application.Interfaces.IRepositories;
using HealthGuard.Application.Interfaces.IServices;
using HealthGuard.Infrastructure.Data;
using HealthGuard.Infrastructure.Identity;
using HealthGuard.Infrastructure.Repositories;
using HealthGuard.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthGuard.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // DB Contexts
            services.AddDbContext<IdentityDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            );

            // Services
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IIdentityService, IdentityService>();

            // Repositories
            services.AddScoped<IImageRepository, ImageRepository>();

            return services;
        }
    }
}
