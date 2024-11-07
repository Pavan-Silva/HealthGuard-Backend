using AutoMapper;
using HealthGuard.Application.DTOs.Disease;
using HealthGuard.Core.Entities.Disease;

namespace HealthGuard.Application.Mappings
{
    public class DiseaseProfile : Profile
    {
        public DiseaseProfile()
        {
            CreateMap<CreateDiseaseDto, Disease>()
                .ForMember(d => d.Symptoms, opt => opt.Ignore())
                .ForMember(d => d.Treatments, opt => opt.Ignore())
                .ForMember(d => d.TransmissionMethods, opt => opt.Ignore());
        }
    }
}
