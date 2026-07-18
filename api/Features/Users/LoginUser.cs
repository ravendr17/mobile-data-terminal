using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MobileDataTerminal.Api.Authentication;
using MobileDataTerminal.Api.Data;
using MobileDataTerminal.Api.Exceptions;

namespace MobileDataTerminal.Api.Features.Users;

public static class LoginUser
{
    private record LoginUserRequest(
        string UsernameOrEmail,
        string Password
    );

    public static void MapLoginUser(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/login", async (
            LoginUserRequest req,
            AppDbContext context,
            PasswordHasher<User> hasher,
            TokenProvider tokenProvider) =>
        {
            var user = await context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == req.UsernameOrEmail ||
                                          u.Email == req.UsernameOrEmail);

            if (user is null)
            {
                throw new UnauthorizedException("Invalid username/email or password");
            }

            var verifyPassword = hasher.VerifyHashedPassword(
                user, user.Password, req.Password);

            if (verifyPassword == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedException("Invalid username/email or password.");
            }

            var token = tokenProvider.Create(user);

            return Results.Ok(token);
        });
    }
}