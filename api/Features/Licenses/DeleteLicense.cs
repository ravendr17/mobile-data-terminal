using Microsoft.EntityFrameworkCore;
using MobileDataTerminal.Api.Data;
using MobileDataTerminal.Api.Exceptions;

namespace MobileDataTerminal.Api.Features.Licenses;

public static class DeleteLicense
{
    public static void MapDeleteLicense(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/licenses/{licenseId:int}", async (
            int licenseId,
            AppDbContext context) =>
        {
            var license = await context.Licenses
                .FirstOrDefaultAsync(l => l.Id == licenseId);

            if (license is null)
            {
                throw new ResourceNotFoundException($"License ID {licenseId} not found.");
            }

            context.Licenses.Remove(license);
            await context.SaveChangesAsync();
            
            return Results.NoContent();
        });
    }
}