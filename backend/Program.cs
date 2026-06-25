using System.Text;
using Backend.Authentication;
using Backend.Data;
using Backend.Entities;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = new Dictionary<string, string>();

        foreach (var entry in context.ModelState)
        {
            if (entry.Key == "request") continue;
            if (entry.Value?.Errors.Count == 0) continue;

            string field = entry.Key.StartsWith("$.") ? entry.Key[2..] : entry.Key;
            string message = entry.Value!.Errors[0].ErrorMessage;

            if (message.Contains("JSON value could not be converted"))
            {
                message = $"{field} has an invalid value or format.";
            }
            errors[field] = message;
        }
        return new BadRequestObjectResult(new { errors });
    };
});

var dbConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(dbConnectionStr)
           .UseSnakeCaseNamingConvention()
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)
            ),
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero,
            RoleClaimType = "role"
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddSingleton<PasswordHasher<Account>>();
builder.Services.AddSingleton<TokenProvider>();

builder.Services.AddScoped<LicenseService>();
builder.Services.AddScoped<VehicleService>();
builder.Services.AddScoped<AccountService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();