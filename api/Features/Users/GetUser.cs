using Microsoft.EntityFrameworkCore;
using MobileDataTerminal.Api.Data;
using MobileDataTerminal.Api.Exceptions;

namespace MobileDataTerminal.Api.Features.Users;

public static class GetUser
{
    private record GetUserResponse(
        int Id,
        string Username,
        string Email,
        int RoleId,
        string Role,
        string? LicenseNumber
    );

    public static void MapGetUser(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users/{username}", async (
            string username,
            AppDbContext context) =>
        {
            var response = await context.Users
                .Where(u => u.Username == username)
                .Select(u => new GetUserResponse(
                    u.Id,
                    u.Username,
                    u.Email,
                    u.RoleId,
                    u.Role.Name,
                    u.License!.Number
                ))
                .FirstOrDefaultAsync();

            if (response is null)
            {
                throw new ResourceNotFoundException($"User {username} not found.");
            }

            return Results.Ok(response);
        });
    }
}