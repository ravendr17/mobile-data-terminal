using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MobileDataTerminal.Api.Exceptions;

namespace MobileDataTerminal.Api.Features.Licenses;

public static class CreateLicense
{
    public record Request(
        string LicenseNumber,
        int TypeId,
        int StatusId,
        DateOnly IssuanceDate,
        int ValidityPeriod,
        string FirstName,
        string? MiddleName,
        string LastName,
        int SexId,
        DateOnly DateOfBirth,
        string Address,
        int NationalityId,
        int EyeColorId,
        int Height,
        int Weight,
        int BloodTypeId
    );
    
    private record Response(
        int Id,
        string LicenseNumber,
        int TypeId,
        string Type,
        int StatusId,
        string Status,
        DateOnly IssuanceDate,
        DateOnly ExpiryDate,
        string FirstName,
        string? MiddleName,
        string LastName,
        int SexId,
        string Sex,
        DateOnly DateOfBirth,
        string Address,
        int NationalityId,
        string Nationality,
        int EyeColorId,
        string EyeColor,
        int Height,
        int Weight,
        int BloodTypeId,
        string BloodType
    );

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.LicenseNumber)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.TypeId)
                .GreaterThan(0);
            RuleFor(x => x.StatusId)
                .GreaterThan(0);
            RuleFor(x => x.IssuanceDate)
                .GreaterThan(new DateOnly(1990, 1, 1))
                .LessThan(DateOnly.FromDateTime(DateTime.UtcNow));
            RuleFor(x => x.ValidityPeriod)
                .Must(x => x is 5 or 10);
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.MiddleName)
                .MaximumLength(50);
            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.SexId)
                .GreaterThan(0);
            RuleFor(x => x.DateOfBirth)
                .GreaterThan(new DateOnly(1900, 1, 1))
                .LessThan(DateOnly.FromDateTime(DateTime.UtcNow));
            RuleFor(x => x.Address)
                .NotEmpty()
                .MaximumLength(250);
            RuleFor(x => x.NationalityId)
                .GreaterThan(0);
            RuleFor(x => x.EyeColorId)
                .GreaterThan(0);
            RuleFor(x => x.Height)
                .InclusiveBetween(1, 999);
            RuleFor(x => x.Weight)
                .InclusiveBetween(1, 999);
            RuleFor(x => x.BloodTypeId)
                .GreaterThan(0);
        }
    }

    public static void MapCreateLicense(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/licenses", async (
            Request req,
            IValidator<Request> validator,
            LicensesDbContext context) =>
        {
            var validationResult = await validator.ValidateAsync(req);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            License license = new License
            {
                Number = req.LicenseNumber,
                TypeId = req.TypeId,
                StatusId = req.StatusId,
                IssuanceDate = req.IssuanceDate,
                ExpiryDate = req.IssuanceDate.AddYears(req.ValidityPeriod),
                FirstName = req.FirstName,
                MiddleName = req.MiddleName,
                LastName = req.LastName,
                SexId = req.SexId,
                DateOfBirth = req.DateOfBirth,
                Address = req.Address,
                NationalityId = req.NationalityId,
                EyeColorId = req.EyeColorId,
                Height = req.Height,
                Weight = req.Weight,
                BloodTypeId = req.BloodTypeId,
            };

            try
            {
                context.Licenses.Add(license);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) 
                when (ex.InnerException is SqlException sqlEx)
            {
                string message = sqlEx.Message;
                
                if (message.Contains("uq_licenses_number"))
                {
                    throw new ConflictException($"License {req.LicenseNumber} already exists.");   
                }
                if (message.Contains("fk_licenses_types"))
                {
                    throw new ResourceNotFoundException($"License type ID {req.TypeId} not found.");
                }
                if (message.Contains("fk_licenses_statuses"))
                {
                    throw new ResourceNotFoundException($"License status ID {req.StatusId} not found.");
                }
                if (message.Contains("fk_licenses_nationalities"))
                {
                    throw new ResourceNotFoundException($"Nationality ID {req.NationalityId} not found.");
                }
                if (message.Contains("fk_licenses_sexes"))
                {
                    throw new ResourceNotFoundException($"Sex ID {req.SexId} not found.");
                }
                if (message.Contains("fk_licenses_eye_colors"))
                {
                    throw new ResourceNotFoundException($"Eye color ID {req.EyeColorId} not found.");
                }
                if (message.Contains("fk_licenses_blood_types"))
                {
                    throw new ResourceNotFoundException($"Blood type ID {req.BloodTypeId} not found.");
                }
                
                throw;
            }
            
            var response = await context.Licenses
                .Where(l => l.Id == license.Id)
                .Select(l => new Response(
                    l.Id,
                    l.Number,
                    l.TypeId,
                    l.Type.Name,
                    l.StatusId,
                    l.Status.Name,
                    l.IssuanceDate,
                    l.ExpiryDate,
                    l.FirstName,
                    l.MiddleName,
                    l.LastName,
                    l.SexId,
                    l.Sex.Name,
                    l.DateOfBirth,
                    l.Address,
                    l.NationalityId,
                    l.Nationality.Name,
                    l.EyeColor.Id,
                    l.EyeColor.Name,
                    l.Height,
                    l.Weight,
                    l.BloodTypeId,
                    l.BloodType.Name
                ))
                .FirstAsync();
            
            return Results.Created($"/api/licenses/{response.Id}", response);
        });
    }
}