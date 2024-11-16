using HealthGuard.Application.DTOs;
using HealthGuard.Application.DTOs.Auth;
using HealthGuard.Core.Entities;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HealthGuard.Application.Configs
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfig(this IServiceCollection services)
        {
            TypeAdapterConfig<User, UserDTO>
                .NewConfig()
                .Map(dest => dest.FirstName, src => src.UserName!.Split('-', StringSplitOptions.None)[0])
                .Map(dest => dest.LastName, src => src.UserName!.Split('-', StringSplitOptions.None)[1])
                .Map(dest => dest.Disabled, src => src.LockoutEnd <= DateTimeOffset.UtcNow);

            TypeAdapterConfig<RegisterUserRequest, User>
                .NewConfig()
                .Map(dest => dest.UserName, src => $"{src.FirstName}-{src.LastName}");

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}
