using Microsoft.EntityFrameworkCore;
using MobileDataTerminal.Api.Exceptions;

namespace MobileDataTerminal.Api.Features.Licenses;

public static class DeleteLicense
{
    private static async Task Handler(int licenseId, LicensesDbContext context)
    {
        var license = await context.Licenses
            .FirstOrDefaultAsync(l => l.Id == licenseId);

        if (license is null)
        {
            throw new ResourceNotFoundException($"License ID {licenseId} not found.");
        }

        context.Licenses.Remove(license);
        await context.SaveChangesAsync();
    }

    public static void MapDeleteLicense(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/licenses/{licenseId:int}", async (
            int licenseId,
            LicensesDbContext context) =>
        {
            await Handler(licenseId, context);

            return Results.NoContent();
        });
    }
}