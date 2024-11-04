using HealthGuard.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace HealthGuard.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile));

            return services;
        }
    }
}
