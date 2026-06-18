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
    }
}