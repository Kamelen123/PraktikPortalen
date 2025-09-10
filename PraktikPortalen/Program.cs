using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PraktikPortalen.Application.Mapping;
using PraktikPortalen.Application.Services;
using PraktikPortalen.Application.Services.Interfaces;
using PraktikPortalen.Domain.Interfaces.Repositories;
using PraktikPortalen.Infrastructure.Data;
using PraktikPortalen.Infrastructure.Repositories;

namespace PraktikPortalen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1) Infrastructure: DbContext
            builder.Services.AddDbContext<PraktikportalenDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // 2) Application: AutoMapper + Services/Repositories
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
            builder.Services.AddScoped<IInternshipRepository, InternshipRepository>();
            builder.Services.AddScoped<IInternshipService, InternshipService>();

            // 3) Web API plumbing
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
