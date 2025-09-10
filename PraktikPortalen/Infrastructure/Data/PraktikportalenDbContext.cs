using Microsoft.EntityFrameworkCore;
using PraktikPortalen.Domain.Entities;
using PraktikPortalen.Domain.Enums;

namespace PraktikPortalen.Infrastructure.Data
{
    public class PraktikportalenDbContext : DbContext
    {
        public PraktikportalenDbContext(DbContextOptions<PraktikportalenDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Internship> Internships => Set<Internship>();
        public DbSet<InternshipApplication> InternshipApplications => Set<InternshipApplication>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---- Fixed timestamps (UTC) ----
            var t0 = new DateTime(2025, 09, 10, 12, 00, 00, DateTimeKind.Utc);
            var deadline1 = new DateTime(2025, 10, 10, 12, 00, 00, DateTimeKind.Utc); // ~+30d
            var deadline2 = new DateTime(2025, 09, 25, 12, 00, 00, DateTimeKind.Utc); // ~+15d

            // User
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Company
            modelBuilder.Entity<Company>()
                .HasIndex(c => c.Name)
                .IsUnique();

            // Category
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            // Internship
            modelBuilder.Entity<Internship>()
                .HasOne(i => i.Company)
                .WithMany(c => c.Internships)
                .HasForeignKey(i => i.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Internship>()
                .HasOne(i => i.Category)
                .WithMany(c => c.Internships)
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Internship>()
                .HasIndex(i => new { i.IsOpen, i.ApplicationDeadline });

            // InternshipApplication
            modelBuilder.Entity<InternshipApplication>()
                .HasOne(a => a.Internship)
                .WithMany(i => i.InternshipApplications)
                .HasForeignKey(a => a.InternshipId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InternshipApplication>()
                .HasOne(a => a.Applicant)
                .WithMany(u => u.InternshipApplications)
                .HasForeignKey(a => a.ApplicantId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prevent duplicate applications for the same internship by the same user
            modelBuilder.Entity<InternshipApplication>()
                .HasIndex(a => new { a.InternshipId, a.ApplicantId })
                .IsUnique();

            // --- Users (hashed passwords) ---
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "admin@test.com",
                    PasswordHash = "AQAAAAIAAYagAAAAEHfdPaPu3AoXt73wEtI9kk74dORAiPsgVJVbKJDfU6UNi2wjuO11LCYGHrCwUxlthQ==", // Admin123!
                    FullName = "Admin User",
                    Role = UserRole.Admin,
                    IsActive = true,
                    CreatedAt = t0
                },
                new User
                {
                    Id = 2,
                    Email = "member1@test.com",
                    PasswordHash = "AQAAAAIAAYagAAAAEJHOQD8WJ9FhT7jFt5WPjdw+iA6FmLgQSsWA+9ranctpdC3Xy2v4vtign4B+sADe+g==", // User123!
                    FullName = "Member One",
                    Role = UserRole.Member,
                    IsActive = true,
                    CreatedAt = t0
                },
                new User
                {
                    Id = 3,
                    Email = "member2@test.com",
                    PasswordHash = "AQAAAAIAAYagAAAAEJHOQD8WJ9FhT7jFt5WPjdw+iA6FmLgQSsWA+9ranctpdC3Xy2v4vtign4B+sADe+g==", // User123!
                    FullName = "Member Two",
                    Role = UserRole.Member,
                    IsActive = true,
                    CreatedAt = t0
                }
            );

            // --- Companies ---
            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "TechCorp", Website = "https://techcorp.com", City = "Göteborg", IsActive = true },
                new Company { Id = 2, Name = "GreenEnergy", Website = "https://greenenergy.com", City = "Stockholm", IsActive = true }
            );

            // --- Categories ---
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "IT", IsActive = true },
                new Category { Id = 2, Name = "Energy", IsActive = true }
            );

            // --- Internships ---
            modelBuilder.Entity<Internship>().HasData(
                new Internship
                {
                    Id = 1,
                    Title = "Backend Developer Intern",
                    CompanyId = 1,
                    CategoryId = 1,
                    LocationType = LocationType.Remote,
                    City = "Göteborg",
                    ApplicationDeadline = deadline1,
                    IsOpen = true,
                    Description = "Work on APIs"
                },
                new Internship
                {
                    Id = 2,
                    Title = "Energy Analyst Intern",
                    CompanyId = 2,
                    CategoryId = 2,
                    LocationType = LocationType.OnSite,
                    City = "Stockholm",
                    ApplicationDeadline = deadline2,
                    IsOpen = true,
                    Description = "Research renewable energy"
                }
            );

            // --- Applications ---
            modelBuilder.Entity<InternshipApplication>().HasData(
                new InternshipApplication
                {
                    Id = 1,
                    InternshipId = 1,
                    ApplicantId = 2,
                    Status = ApplicationStatus.Submitted,
                    CoverLetter = "Excited to join",
                    CvUrl = "cv_member1.pdf",
                    SubmittedAt = t0
                },
                new InternshipApplication
                {
                    Id = 2,
                    InternshipId = 2,
                    ApplicantId = 3,
                    Status = ApplicationStatus.Submitted,
                    CoverLetter = "Passionate about energy",
                    CvUrl = "cv_member2.pdf",
                    SubmittedAt = t0
                }
            );
        }
    }
}
