using AutoMapper;
using PraktikPortalen.Application.DTOs.Companies;
using PraktikPortalen.Application.DTOs.Internships;
using PraktikPortalen.Domain.Entities;

namespace PraktikPortalen.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Internship, InternshipListDto>()
                .ForMember(d => d.CompanyName, m => m.MapFrom(s => s.Company.Name))
                .ForMember(d => d.CategoryName, m => m.MapFrom(s => s.Category.Name));

            CreateMap<Internship, InternshipDetailDto>()
                .ForMember(d => d.CompanyName, m => m.MapFrom(s => s.Company.Name))
                .ForMember(d => d.CategoryName, m => m.MapFrom(s => s.Category.Name))
                .ForMember(d => d.LocationType, m => m.MapFrom(s => s.LocationType.ToString()));

            CreateMap<InternshipCreateDto, Internship>();
            CreateMap<InternshipUpdateDto, Internship>();

            CreateMap<Company, CompanyListDto>();
            CreateMap<Company, CompanyDetailDto>();
            CreateMap<CompanyCreateDto, Company>();
            CreateMap<CompanyUpdateDto, Company>();
        }
    }
}
