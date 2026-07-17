using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MobileDataTerminal.Api.Data;
using MobileDataTerminal.Api.Exceptions;

namespace MobileDataTerminal.Api.Features.Users;

public static class CreateUser
{
    public record Request(
        string Username,
        string Email,
        string Password,
        int RoleId,
        string? LicenseNumber
    );

    private record Response(
        int Id,
        string Username,
        string Email,
        int RoleId,
        string Role,
        string? LicenseNumber
    );

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(30);
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(12)
                .MaximumLength(255);
            RuleFor(x => x.RoleId)
                .GreaterThan(0);
            RuleFor(x => x.LicenseNumber)
                .MaximumLength(30);
        }
    }

    public static void MapCreateUser(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/users", async (
            Request req,
            IValidator<Request> validator,
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

            User user = new User
            {
                Username = req.Username,
                Email = req.Email,
                Password = req.Password,
                RoleId = req.RoleId,
                LicenseId = licenseId
            };

            try
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is SqlException sqlEx)
            {
                string message = sqlEx.Message;

                if (message.Contains("uq_users_username"))
                {
                    throw new ConflictException($"Username {req.Username} is already taken.");
                }
                if (message.Contains("uq_users_email"))
                {
                    throw new ConflictException($"Email {req.Email} is already registered.");   
                }
                if (message.Contains("uq_users_license_id"))
                {
                    throw new ConflictException($"License {req.LicenseNumber} is already linked to another user.");
                }
                if (message.Contains("fk_users_user_roles"))
                {
                    throw new ResourceNotFoundException($"User role ID {req.RoleId} not found.");
                }

                throw;
            }

            var response = await context.Users
                .Where(u => u.Id == user.Id)
                .Select(u => new Response(
                    u.Id,
                    u.Username,
                    u.Email,
                    u.RoleId,
                    u.Role.Name,
                    u.License != null ? u.License.Number : null
                ))
                .FirstAsync();

            return Results.Created($"/api/users/{response.Id}", response);
        });
    }
}