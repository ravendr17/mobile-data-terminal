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
        
        modelBuilder.Entity<License>(entity =>
        {
            entity.Property(e => e.Number).HasMaxLength(30);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Sex).HasMaxLength(10);
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Nationality).HasMaxLength(30);
            entity.Property(e => e.EyeColor).HasMaxLength(30);
            entity.Property(e => e.BloodType).HasMaxLength(10);

            entity.HasIndex(e => e.Number)
                .IsUnique().HasDatabaseName("uq_licenses_number");
            
            entity.HasOne(l => l.Type)
                .WithMany()
                .HasForeignKey(l => l.TypeId)
                .HasConstraintName("fk_licenses_license_types");
            
            entity.HasOne(l => l.Status)
                .WithMany()
                .HasForeignKey(l => l.StatusId)
                .HasConstraintName("fk_licenses_license_statuses");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.Property(e => e.PlateNumber).HasMaxLength(30);
            entity.Property(e => e.MvFileNumber).HasMaxLength(30);
            entity.Property(e => e.Vin).HasMaxLength(30);
            entity.Property(e => e.Make).HasMaxLength(30);
            entity.Property(e => e.Model).HasMaxLength(30);
            entity.Property(e => e.Color).HasMaxLength(30);
            
            entity.HasIndex(e => e.PlateNumber)
                .IsUnique()
                .HasDatabaseName("uq_vehicles_plate_number");

            entity.HasIndex(e => e.MvFileNumber)
                .IsUnique()
                .HasDatabaseName("uq_vehicles_mv_file_number");
            
            entity.HasIndex(e => e.Vin)
                .IsUnique()
                .HasDatabaseName("uq_vehicles_vin");
            
            entity.HasOne(v => v.License)
                .WithMany(l => l.Vehicles)
                .HasForeignKey(v => v.LicenseId)
                .HasConstraintName("fk_vehicles_licenses");
        });
        
        
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.Username).HasMaxLength(30);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);

            entity.HasIndex(e => e.Username)
                .IsUnique()
                .HasDatabaseName("uq_accounts_username");

            entity.HasIndex(e => e.Email)
                .IsUnique()
                .HasDatabaseName("uq_accounts_email");
            
            entity.HasOne(a => a.Role)
                .WithMany()
                .HasForeignKey(a => a.RoleId)
                .HasConstraintName("fk_accounts_account_roles");
            
            entity.HasOne(a => a.License)
                .WithOne()
                .HasForeignKey<Account>(a => a.LicenseId)
                .HasConstraintName("fk_accounts_licenses");
        });
        
        modelBuilder.Entity<AccountRole>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(30);
            
            entity.HasIndex(e => e.Name)
                .IsUnique().HasDatabaseName("uq_account_roles_name");
            
            entity.HasData(
                new AccountRole { Id = 1, Name = "Civilian" },
                new AccountRole { Id = 2, Name = "Officer" },
                new AccountRole { Id = 3, Name = "Supervisor" },
                new AccountRole { Id = 4, Name = "Admin" }
            );
        });
        
        modelBuilder.Entity<LicenseType>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(30);
            
            entity.HasIndex(e => e.Name)
                .IsUnique().HasDatabaseName("uq_license_types_name");
            
            entity.HasData(
                new LicenseType { Id = 1, Name = "Non-Professional" },
                new LicenseType { Id = 2, Name = "Professional" },
                new LicenseType { Id = 3, Name = "Student Permit" }
            );
        });
        
        modelBuilder.Entity<LicenseStatus>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(30);
            
            entity.HasIndex(e => e.Name)
                .IsUnique().HasDatabaseName("uq_license_statuses_name");
            
            entity.HasData(
                new LicenseStatus { Id = 1, Name = "Active" },
                new LicenseStatus { Id = 2, Name = "Revoked" },
                new LicenseStatus { Id = 3, Name = "Expired" }
            );
        });
    }
}