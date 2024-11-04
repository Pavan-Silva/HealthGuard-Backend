using HealthGuard.Application.Interfaces.IServices;
using HealthGuard.Infrastructure.Identity;
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

            // Services
            services.AddScoped<IIdentityService, IdentityService>();

            return services;
        }
    }
}
