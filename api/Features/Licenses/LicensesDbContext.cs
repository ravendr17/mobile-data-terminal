using Microsoft.EntityFrameworkCore;

namespace MobileDataTerminal.Api.Features.Licenses;

public class LicensesDbContext(DbContextOptions<LicensesDbContext> options): DbContext(options)
{
    public DbSet<License> Licenses { get; set; }
    public DbSet<LicenseType> LicenseTypes { get; set; }
    public DbSet<LicenseStatus> LicenseStatuses { get; set; }
    public DbSet<Nationality> Nationalities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LicenseType>(e =>
        {
            e.Property(e => e.Name).HasMaxLength(50);
            e.HasIndex(e => e.Name)
                .IsUnique().HasDatabaseName("uq_license_types_name");

            e.HasData(
                new LicenseType {Id = 1, Name = "Professional"},
                new LicenseType {Id = 2, Name = "NonProfessional"},
                new LicenseType {Id = 3, Name = "StudentPermit"}
            );
        });

        modelBuilder.Entity<LicenseStatus>(e =>
        {
            e.Property(e => e.Name).HasMaxLength(50);
            e.HasIndex(e => e.Name)
                .IsUnique().HasDatabaseName("uq_license_statuses_name");

            e.HasData(
                new LicenseStatus {Id = 1, Name = "Active"},
                new LicenseStatus {Id = 2, Name = "Revoked"},
                new LicenseStatus {Id = 3, Name = "Expired"}
            );
        });

        modelBuilder.Entity<Nationality>(e =>
        {
            e.Property(e => e.Name).HasMaxLength(50);
            e.HasIndex(e => e.Name)
                .IsUnique().HasDatabaseName("uq_nationalities_name");

            e.HasData(
                new Nationality {Id = 1, Name = "Filipino"},
                new Nationality {Id = 2, Name = "American"},
                new Nationality {Id = 3, Name = "Japanese"},
                new Nationality {Id = 4, Name = "Chinese"},
                new Nationality {Id = 5, Name = "SouthKorean"},
                new Nationality {Id = 6, Name = "Taiwanese"},
                new Nationality {Id = 7, Name = "Singaporean"},
                new Nationality {Id = 8, Name = "Malaysian"},
                new Nationality {Id = 9, Name = "Indonesian"},
                new Nationality {Id = 10, Name = "Thai"},
                new Nationality {Id = 11, Name = "Cambodian"},
                new Nationality {Id = 12, Name = "Vietnamese"},
                new Nationality {Id = 13, Name = "British"},
                new Nationality {Id = 14, Name = "Australian"},
                new Nationality {Id = 15, Name = "Canadian"},
                new Nationality {Id = 16, Name = "NewZealander"},
                new Nationality {Id = 17, Name = "Spanish"},
                new Nationality {Id = 18, Name = "Portuguese"},
                new Nationality {Id = 19, Name = "Mexican"},
                new Nationality {Id = 20, Name = "German"},
                new Nationality {Id = 21, Name = "Dutch"},
                new Nationality {Id = 22, Name = "French"},
                new Nationality {Id = 23, Name = "Italian"},
                new Nationality {Id = 24, Name = "Swiss"},
                new Nationality {Id = 25, Name = "Swedish"},
                new Nationality {Id = 26, Name = "Norwegian"},
                new Nationality {Id = 27, Name = "Ukrainian"},
                new Nationality {Id = 28, Name = "Russian"},
                new Nationality {Id = 29, Name = "SouthAfrican"},
                new Nationality {Id = 30, Name = "Indian"}
            );
        });

        modelBuilder.Entity<License>(e =>
        {
            e.Property(e => e.Number).HasMaxLength(30);
            e.Property(e => e.FirstName).HasMaxLength(50);
            e.Property(e => e.MiddleName).HasMaxLength(50);
            e.Property(e => e.LastName).HasMaxLength(50);
            e.Property(e => e.Address).HasMaxLength(250);
            
            e.HasIndex(e => e.Number)
                .IsUnique().HasDatabaseName("uq_licenses_number");

            e.HasOne(e => e.Type)
                .WithMany()
                .HasForeignKey(e => e.TypeId)
                .HasConstraintName("fk_licenses_types");

            e.HasOne(e => e.Status)
                .WithMany()
                .HasForeignKey(e => e.StatusId)
                .HasConstraintName("fk_licenses_statuses");

            e.HasOne(e => e.Nationality)
                .WithMany()
                .HasForeignKey(e => e.NationalityId)
                .HasConstraintName("fk_licenses_nationalities");
        });
    }
}