using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PraktikPortalen.Application.Mapping;
using PraktikPortalen.Application.Security;
using PraktikPortalen.Application.Services;
using PraktikPortalen.Application.Services.Interfaces;
using PraktikPortalen.Domain.Entities;

namespace PraktikPortalen.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // AutoMapper profile(s)
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            // Application services
            services.AddScoped<IInternshipService, InternshipService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IInternshipApplicationService, InternshipApplicationService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            return services;
        }
    }
}
