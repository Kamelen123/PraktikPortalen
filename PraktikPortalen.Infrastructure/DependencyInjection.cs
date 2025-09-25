using Microsoft.Extensions.DependencyInjection;
using PraktikPortalen.Domain.Interfaces.Repositories;
using PraktikPortalen.Infrastructure.Repositories;
using PraktikPortalen.Infrastructure.Services;

namespace PraktikPortalen.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Repository registrations live here, not in the API.
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IInternshipRepository, InternshipRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IInternshipApplicationRepository, InternshipApplicationRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddHostedService<InternshipCleanupService>();
            return services;
        }
    }
}
