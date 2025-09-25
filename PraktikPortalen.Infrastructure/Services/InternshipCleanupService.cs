using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PraktikPortalen.Infrastructure.Data;

namespace PraktikPortalen.Infrastructure.Services
{
    public class InternshipCleanupService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<InternshipCleanupService> _logger;

        public InternshipCleanupService(IServiceProvider services, ILogger<InternshipCleanupService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Loop until service is stopped
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _services.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<PraktikportalenDbContext>();

                    var now = DateTime.UtcNow;

                    var expired = await db.Internships
                        .Where(i => i.IsOpen && i.ApplicationDeadline < now)
                        .ToListAsync(stoppingToken);

                    if (expired.Any())
                    {
                        foreach (var internship in expired)
                            internship.IsOpen = false;

                        await db.SaveChangesAsync(stoppingToken);

                        _logger.LogInformation(
                            "Closed {Count} expired internships at {Time}",
                            expired.Count,
                            now
                        );
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in InternshipCleanupService");
                }

                // For demo: run every 5 minutes instead of daily
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
