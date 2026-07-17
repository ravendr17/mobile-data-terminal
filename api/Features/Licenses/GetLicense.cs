using Microsoft.EntityFrameworkCore;
using MobileDataTerminal.Api.Data;
using MobileDataTerminal.Api.Exceptions;

namespace MobileDataTerminal.Api.Features.Licenses;

public static class GetLicense
{
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
    
    public static void MapGetLicense(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/licenses/{licenseNumber}", async (
            string licenseNumber,
            AppDbContext context) =>
        {
            var response = await context.Licenses
                .Where(l => l.Number == licenseNumber)
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
                .FirstOrDefaultAsync();

            if (response is null)
            {
                throw new ResourceNotFoundException($"License {licenseNumber} not found.");
            }
            
            return Results.Ok(response);
        });
    }
}