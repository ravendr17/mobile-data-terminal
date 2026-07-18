using Microsoft.EntityFrameworkCore;
using MobileDataTerminal.Api.Features.Licenses;
using MobileDataTerminal.Api.Features.Ticketing;
using MobileDataTerminal.Api.Features.Users;
using MobileDataTerminal.Api.Features.Vehicles;

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
    
    public DbSet<Vehicle> Vehicles { get; set; }
    
    public DbSet<Violation> Violations { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketStatus> TicketStatuses { get; set; }
    public DbSet<TicketViolation> TicketViolations { get; set; }
    
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

        modelBuilder.Entity<Vehicle>(e =>
        {
            e.Property(e => e.PlateNumber).HasMaxLength(30);
            e.Property(e => e.MvFileNumber).HasMaxLength(30);
            e.Property(e => e.Vin).HasMaxLength(30);
            e.Property(e => e.Make).HasMaxLength(50);
            e.Property(e => e.Model).HasMaxLength(50);
            e.Property(e => e.Color).HasMaxLength(50);
            
            e.HasIndex(e => e.PlateNumber)
                .IsUnique()
                .HasDatabaseName("uq_vehicles_plate_number");

            e.HasIndex(e => e.MvFileNumber)
                .IsUnique()
                .HasDatabaseName("uq_vehicles_mv_file_number");
            
            e.HasIndex(e => e.Vin)
                .IsUnique()
                .HasDatabaseName("uq_vehicles_vin");

            e.HasOne(e => e.License)
                .WithMany(l => l.Vehicles)
                .HasForeignKey(e => e.LicenseId)
                .HasConstraintName("fk_vehicles_licenses");
        });
        
        modelBuilder.Entity<Violation>(e =>
        { 
            e.Property(e => e.Name).HasMaxLength(100);
            
            e.HasIndex(e => e.Name).IsUnique()
                .HasDatabaseName("uq_violations_name");

            e.HasData(
                new Violation {Id = 1, Name = "DRIVING WITHOUT VALID LICENSE", IsTiered = false, InitialFine = 3000},
                new Violation {Id = 2, Name = "FAILURE TO CARRY LICENSE", IsTiered = false, InitialFine = 1000},
                new Violation {Id = 3, Name = "FAKE DRIVER'S LICENSE", IsTiered = false, InitialFine = 3000},
                new Violation {Id = 4, Name = "DRIVING UNREGISTERED VEHICLE", IsTiered = false, InitialFine = 10000},
                new Violation {Id = 5, Name = "ILLEGAL MODIFICATIONS", IsTiered = false, InitialFine = 5000},
                new Violation {Id = 6, Name = "DEFECTIVE/IMPROPER EQUIPMENT", IsTiered = false, InitialFine = 5000},
                new Violation {Id = 7, Name = "RECKLESS DRIVING", IsTiered = true, InitialFine = 2000, SecondFine = 3000, ThirdFine = 10000},
                new Violation {Id = 8, Name = "NO SEATBELT", IsTiered = true, InitialFine = 1000, SecondFine = 3000, ThirdFine = 5000},
                new Violation {Id = 9, Name = "NO HELMET", IsTiered = true, InitialFine = 1500, SecondFine = 3000, ThirdFine = 5000},
                new Violation {Id = 10, Name = "DRIVING UNDER INFLUENCE", IsTiered = true, InitialFine = 20000, SecondFine = 50000, ThirdFine = 100000},
                new Violation {Id = 11, Name = "OBSTRUCTION", IsTiered = false, InitialFine = 1000},
                new Violation {Id = 12, Name = "NO OR/CR", IsTiered = false, InitialFine = 1000},
                new Violation {Id = 13, Name = "OVERLOADING PASSENGERS", IsTiered = false, InitialFine = 2000},
                new Violation {Id = 14, Name = "OVER-SPEEDING", IsTiered = false, InitialFine = 1000},
                new Violation {Id = 15, Name = "BEATING THE RED LIGHT", IsTiered = false, InitialFine = 1000},
                new Violation {Id = 16, Name = "ILLEGAL PARKING", IsTiered = false, InitialFine = 1000},
                new Violation {Id = 17, Name = "USING PHONE WHILE DRIVING", IsTiered = false, InitialFine = 1000},
                new Violation {Id = 18, Name = "COUNTERFLOWING", IsTiered = false, InitialFine = 2000}
            );

            e.Property(e => e.InitialFine).HasPrecision(10, 2);
            e.Property(e => e.SecondFine).HasPrecision(10, 2);
            e.Property(e => e.ThirdFine).HasPrecision(10, 2);
        });
        
        modelBuilder.Entity<TicketStatus>(e =>
        {
            e.Property(e => e.Name).HasMaxLength(50);
            e.HasIndex(e => e.Name)
                .IsUnique().HasDatabaseName("uq_ticket_statuses_name");

            e.HasData(
                new TicketStatus {Id = 1, Name = "Unsettled"},
                new TicketStatus {Id = 2, Name = "Settled"}
            );
        });

        modelBuilder.Entity<Ticket>(e =>
        {
            e.Property(e => e.ReferenceNumber).HasMaxLength(30);
            e.Property(e => e.IncidentPlace).HasMaxLength(250);
            e.Property(e => e.OfficerNotes).HasMaxLength(250);

            e.HasIndex(e => e.ReferenceNumber)
                .IsUnique().HasDatabaseName("uq_tickets_reference_number");

            e.HasOne(e => e.License)
                .WithMany()
                .HasForeignKey(e => e.LicenseId)
                .HasConstraintName("fk_tickets_licenses");

            e.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        });
        
        modelBuilder.Entity<TicketViolation>(e =>
        {
            e.HasKey(e => new { e.TicketId, e.ViolationId });
            
            e.HasOne(e => e.Ticket)
                .WithMany(t => t.TicketViolations)
                .HasForeignKey(e => e.TicketId)
                .HasConstraintName("fk_ticket_violations_tickets");

            e.HasOne(e => e.Violation)
                .WithMany()
                .HasForeignKey(e => e.ViolationId)
                .HasConstraintName("fk_ticket_violations_violations");

            e.Property(e => e.Fine).HasPrecision(10, 2);
        });    
    }
}