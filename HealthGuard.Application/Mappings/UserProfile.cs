using AutoMapper;
using HealthGuard.Application.DTOs;
using HealthGuard.Core.Entities;

namespace HealthGuard.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(
                dst => dst.FirstName,
                opts => opts.MapFrom(src => src.UserName!.Split("-", StringSplitOptions.None)[0]))
                .ForMember(
                dst => dst.LastName,
                opts => opts.MapFrom(src => src.UserName!.Split("-", StringSplitOptions.None)[1]))
                .ForMember(
                dst => dst.Disabled,
                opts => opts.MapFrom(src => src.LockoutEnd <= DateTimeOffset.UtcNow));
        }
    }
}
