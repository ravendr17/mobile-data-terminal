using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MobileDataTerminal.Api.Data;
using MobileDataTerminal.Api.Exceptions;

namespace MobileDataTerminal.Api.Features.Vehicles;

public static class CreateVehicle
{
    public record CreateVehicleRequest(
        string PlateNumber,
        string? MvFileNumber,
        string Vin,
        DateOnly RegisterIssuanceDate,
        int ValidityPeriod,
        string Make,
        string Model,
        int Year,
        string Color,
        string? LicenseNumber
    );

    private record CreateVehicleResponse(
        int Id,
        string PlateNumber,
        string MvFileNumber,
        string Vin,
        DateOnly RegisterIssuanceDate,
        DateOnly RegisterExpiryDate,
        string Make,
        string Model,
        int Year,
        string Color,
        string? LicenseNumber
    );

    public class Validator : AbstractValidator<CreateVehicleRequest>
    {
        public Validator()
        {
            RuleFor(x => x.PlateNumber)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.MvFileNumber)
                .MaximumLength(30);
            RuleFor(x => x.Vin)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.RegisterIssuanceDate)
                .GreaterThanOrEqualTo(new DateOnly(1990, 1, 1))
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow));
            RuleFor(x => x.ValidityPeriod)
                .Must(x => x is 1 or 3);
            RuleFor(x => x.Make)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Model)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Year)
                .GreaterThanOrEqualTo(1900)
                .LessThanOrEqualTo(DateTime.Now.Year);
            RuleFor(x => x.Color)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.LicenseNumber)
                .MaximumLength(30);
        }
    }

    public static void MapCreateVehicle(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/vehicles", async (
            CreateVehicleRequest req,
            IValidator<CreateVehicleRequest> validator,
            AppDbContext context) =>
        {
            var validationResult = await validator.ValidateAsync(req);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            int? licenseId = null;

            if (!string.IsNullOrWhiteSpace(req.LicenseNumber))
            {
                var license = await context.Licenses
                    .AsNoTracking()
                    .FirstOrDefaultAsync(l => l.Number == req.LicenseNumber);

                if (license is null)
                {
                    throw new ResourceNotFoundException($"License {req.LicenseNumber} not found.");
                }

                licenseId = license.Id;
            }

            Vehicle vehicle = new Vehicle
            {
                PlateNumber = req.PlateNumber,
                MvFileNumber = req.MvFileNumber,
                Vin = req.Vin,
                RegisterIssuanceDate = req.RegisterIssuanceDate,
                RegisterExpiryDate = req.RegisterIssuanceDate.AddYears(req.ValidityPeriod),
                Make = req.Make,
                Model = req.Model,
                Year = req.Year,
                Color = req.Color,
                LicenseId = licenseId
            };

            try
            {
                context.Vehicles.Add(vehicle);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is SqlException sqlEx)
            {
                string message = sqlEx.Message;

                if (message.Contains("uq_vehicles_plate_number"))
                {
                    throw new ConflictException($"Vehicle with plate {req.PlateNumber} already exists.");
                }
                if (message.Contains("uq_vehicles_mv_file_number"))
                {
                    throw new ConflictException($"Vehicle with MV File {req.MvFileNumber} already exists.");
                }
                if (message.Contains("uq_vehicles_vin"))
                {
                    throw new ConflictException($"Vehicle with VIN {req.Vin} already exists.");
                }

                throw;
            }

            var response = await context.Vehicles
                .Where(v => v.Id == vehicle.Id)
                .Select(v => new CreateVehicleResponse(
                    v.Id,
                    v.PlateNumber,
                    v.MvFileNumber!,
                    v.Vin,
                    v.RegisterIssuanceDate,
                    v.RegisterExpiryDate,
                    v.Make,
                    v.Model,
                    v.Year,
                    v.Color,
                    v.License!.Number
                ))
                .FirstAsync();

            return Results.Created($"/api/vehicles/{response.Id}", response);
        });
    }
}