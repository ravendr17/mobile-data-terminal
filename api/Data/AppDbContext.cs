using Microsoft.EntityFrameworkCore;
using MobileDataTerminal.Api.Features.Licenses;
using MobileDataTerminal.Api.Features.Users;

namespace MobileDataTerminal.Api.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
{
    public DbSet<License> Licenses { get; set; }
    public DbSet<LicenseType> LicenseTypes { get; set; }
    public DbSet<LicenseStatus> LicenseStatuses { get; set; }
    public DbSet<Sex> Sexes { get; set; }
    public DbSet<EyeColor> EyeColors { get; set; }
    public DbSet<BloodType> BloodTypes { get; set; }
    public DbSet<Nationality> Nationalities { get; set; }
    
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    
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

        modelBuilder.Entity<Sex>(e =>
        {
            e.Property(e => e.Name).HasMaxLength(50);
            e.HasIndex(e => e.Name)
                .IsUnique().HasDatabaseName("uq_sexes_name");

            e.HasData(
                new Sex {Id = 1, Name = "Male"},
                new Sex {Id = 2, Name = "Female"}
            );
        });

        modelBuilder.Entity<EyeColor>(e =>
        {
            e.Property(e => e.Name).HasMaxLength(50);
            e.HasIndex(e => e.Name)
                .IsUnique().HasDatabaseName("uq_eye_colors_name");
            
            e.HasData(
                new EyeColor {Id = 1, Name = "Brown"},
                new EyeColor {Id = 2, Name = "Blue"},
                new EyeColor {Id = 3, Name = "Green"},
                new EyeColor {Id = 4, Name = "Hazel"},
                new EyeColor {Id = 5, Name = "Gray"},
                new EyeColor {Id = 6, Name = "Amber"}
            );
        });
        
        modelBuilder.Entity<BloodType>(e =>
        {
            e.Property(e => e.Name).HasMaxLength(50);
            e.HasIndex(e => e.Name)
                .IsUnique().HasDatabaseName("uq_blood_types_name");
            
            e.HasData(
                new BloodType {Id = 1, Name = "A+"},
                new BloodType {Id = 2, Name = "A-"},
                new BloodType {Id = 3, Name = "B+"},
                new BloodType {Id = 4, Name = "B-"},
                new BloodType {Id = 5, Name = "AB+"},
                new BloodType {Id = 6, Name = "AB-"},
                new BloodType {Id = 7, Name = "O+"},
                new BloodType {Id = 8, Name = "O-"}
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
            e.Property(e => e.FirstName).HasMaxLength(100);
            e.Property(e => e.MiddleName).HasMaxLength(100);
            e.Property(e => e.LastName).HasMaxLength(100);
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

            e.HasOne(e => e.Sex)
                .WithMany()
                .HasForeignKey(e => e.SexId)
                .HasConstraintName("fk_licenses_sexes");

            e.HasOne(e => e.EyeColor)
                .WithMany()
                .HasForeignKey(e => e.EyeColorId)
                .HasConstraintName("fk_licenses_eye_colors");

            e.HasOne(e => e.BloodType)
                .WithMany()
                .HasForeignKey(e => e.BloodTypeId)
                .HasConstraintName("fk_licenses_blood_types");
        });
        
        modelBuilder.Entity<UserRole>(e =>
        {
            e.Property(e => e.Name).HasMaxLength(50);
            e.HasIndex(e => e.Name)
                .IsUnique().HasDatabaseName("uq_user_roles_name");

            e.HasData(
                new UserRole {Id = 1, Name = "Civilian"},
                new UserRole {Id = 2, Name = "Officer"},
                new UserRole {Id = 3, Name = "Supervisor"},
                new UserRole {Id = 4, Name = "Admin"}
            );
        });

        modelBuilder.Entity<User>(e =>
        {
            e.Property(e => e.Username).HasMaxLength(30);
            e.Property(e => e.Email).HasMaxLength(100);
            e.Property(e => e.Password).HasMaxLength(255);

            e.HasIndex(e => e.Username)
                .IsUnique().HasDatabaseName("uq_users_username");

            e.HasIndex(e => e.Email)
                .IsUnique().HasDatabaseName("uq_users_email");

            e.HasIndex(e => e.LicenseId)
                .IsUnique().HasDatabaseName("uq_users_license_id");
            
            e.HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey(e => e.RoleId)
                .HasConstraintName("fk_users_user_roles");

            e.HasOne(e => e.License)
                .WithOne()
                .HasForeignKey<User>(e => e.LicenseId)
                .HasConstraintName("fk_users_licenses");
        });
    }
}