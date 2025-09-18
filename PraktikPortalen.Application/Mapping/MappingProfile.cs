using AutoMapper;
using PraktikPortalen.Application.DTOs.Companies;
using PraktikPortalen.Application.DTOs.Internships;
using PraktikPortalen.Application.DTOs.Users;
using PraktikPortalen.Domain.Entities;

namespace PraktikPortalen.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ------- Internship mappings -------
            CreateMap<Internship, InternshipListDto>()
                .ForMember(d => d.CompanyName, m => m.MapFrom(s => s.Company.Name))
                .ForMember(d => d.CategoryName, m => m.MapFrom(s => s.Category.Name));

            CreateMap<Internship, InternshipDetailDto>()
                .ForMember(d => d.CompanyName, m => m.MapFrom(s => s.Company.Name))
                .ForMember(d => d.CategoryName, m => m.MapFrom(s => s.Category.Name))
                .ForMember(d => d.LocationType, m => m.MapFrom(s => s.LocationType.ToString()));

            CreateMap<InternshipCreateDto, Internship>();
            CreateMap<InternshipUpdateDto, Internship>();

            // ------- Company mappings -------
            CreateMap<Company, CompanyListDto>();
            CreateMap<Company, CompanyDetailDto>();
            CreateMap<CompanyCreateDto, Company>();
            CreateMap<CompanyUpdateDto, Company>();

            // ------- User mappings -------
            CreateMap<User, UserListDto>()
            .ForMember(d => d.Role, m => m.MapFrom(s => s.Role.ToString()));

            CreateMap<User, UserDetailDto>()
                .ForMember(d => d.Role, m => m.MapFrom(s => s.Role.ToString()));

            CreateMap<UserCreateDto, User>()
                .ForMember(d => d.PasswordHash, m => m.Ignore()); // password handled separately

            CreateMap<UserUpdateDto, User>();
        }
    }
}
