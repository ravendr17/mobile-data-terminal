using Microsoft.EntityFrameworkCore;
using MobileDataTerminal.Api.Data;
using MobileDataTerminal.Api.Exceptions;

namespace MobileDataTerminal.Api.Features.Users;

public static class DeleteUser
{
    public static void MapDeleteUser(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/users/{userId:int}", async (
            int userId,
            AppDbContext context) =>
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user is null)
            {
                throw new ResourceNotFoundException($"User ID {userId} not found.");
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}