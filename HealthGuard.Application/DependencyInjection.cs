using HealthGuard.Application.Mappings;
using HealthGuard.Application.Services;
using HealthGuard.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HealthGuard.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddServices();
            services.RegisterAutoMapper();
            return services;
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IDiseaseService, DiseaseService>();
            services.AddScoped<ISymptomService, SymptomService>();
            services.AddScoped<ITreatmentService, TreatmentService>();
            services.AddScoped<ITransmissionMethodService, TransmissionMethodService>();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<INotificationService, NotificationService>();
        }

        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile));
        }
    }
}
