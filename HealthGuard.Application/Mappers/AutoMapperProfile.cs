using AutoMapper;
using HealthGuard.Application.DTOs;
using HealthGuard.Domain.Entities;

namespace HealthGuard.Application.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
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
