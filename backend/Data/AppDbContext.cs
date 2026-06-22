using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<License> Licenses { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<LicenseType> LicenseTypes { get; set; }
    public DbSet<LicenseStatus> LicenseStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Account>()
            .HasOne(a => a.License)
            .WithOne()
            .HasForeignKey<Account>(a => a.LicenseId)
            .HasConstraintName("fk_accounts_licenses");

        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.License)
            .WithMany(l => l.Vehicles)
            .HasForeignKey(v => v.LicenseId)
            .HasConstraintName("fk_vehicles_licenses");

        modelBuilder.Entity<Account>()
            .HasOne(a => a.Role)
            .WithMany()
            .HasForeignKey(a => a.RoleId)
            .HasConstraintName("fk_accounts_account_roles");

        modelBuilder.Entity<License>()
            .HasOne(l => l.Type)
            .WithMany()
            .HasForeignKey(l => l.TypeId)
            .HasConstraintName("fk_licenses_license_types");

        modelBuilder.Entity<License>()
            .HasOne(l => l.Status)
            .WithMany()
            .HasForeignKey(l => l.StatusId)
            .HasConstraintName("fk_licenses_license_statuses");
        
        modelBuilder.Entity<LicenseType>().HasData(
            new LicenseType { Id = 1, Name = "Non-Professional" },
            new LicenseType { Id = 2, Name = "Professional" },
            new LicenseType { Id = 3, Name = "Student Permit" }
        );

        modelBuilder.Entity<LicenseStatus>().HasData(
            new LicenseStatus { Id = 1, Name = "Active" },
            new LicenseStatus { Id = 2, Name = "Revoked" },
            new LicenseStatus { Id = 3, Name = "Expired" }
        );

        modelBuilder.Entity<AccountRole>().HasData(
            new AccountRole {Id = 1, Name = "Civilian"},
            new AccountRole {Id = 2, Name = "Officer"},
            new AccountRole {Id = 3, Name = "Supervisor"},
            new AccountRole {Id = 4, Name = "Admin"}
        );
    }
}