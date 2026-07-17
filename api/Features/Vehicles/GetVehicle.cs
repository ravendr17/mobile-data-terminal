using Microsoft.EntityFrameworkCore;
using MobileDataTerminal.Api.Data;
using MobileDataTerminal.Api.Exceptions;

namespace MobileDataTerminal.Api.Features.Vehicles;

public static class GetVehicle
{
    private record GetVehicleResponse(
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

    public static void MapGetVehicle(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/vehicles/{plateOrMvFileNum}", async (
            string plateOrMvFileNum,
            AppDbContext context) =>
        {
            var response = await context.Vehicles
                .Where(v => v.PlateNumber == plateOrMvFileNum ||
                            v.MvFileNumber == plateOrMvFileNum)
                .Select(v => new GetVehicleResponse(
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
                    v.License!.Number))
                .FirstOrDefaultAsync();

            if (response is null)
            {
                throw new ResourceNotFoundException($"Vehicle with Plate/MV File {plateOrMvFileNum} not found.");
            }

            return Results.Ok(response);
        });
    }
}