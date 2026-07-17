using Microsoft.EntityFrameworkCore;
using MobileDataTerminal.Api.Data;
using MobileDataTerminal.Api.Exceptions;

namespace MobileDataTerminal.Api.Features.Vehicles;

public static class DeleteVehicle
{
    public static void MapDeleteVehicle(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/vehicles/{vehicleId:int}", async (
            int vehicleId,
            AppDbContext context) =>
        {
            var vehicle = await context.Vehicles
                .FirstOrDefaultAsync(v => v.Id == vehicleId);

            if (vehicle is null)
            {
                throw new ResourceNotFoundException($"Vehicle ID {vehicleId} not found.");
            }

            context.Vehicles.Remove(vehicle);
            await context.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}