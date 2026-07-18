using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MobileDataTerminal.Api.Authentication;
using MobileDataTerminal.Api.Data;
using MobileDataTerminal.Api.Exceptions;
using MobileDataTerminal.Api.Features.Licenses;
using MobileDataTerminal.Api.Features.Users;
using MobileDataTerminal.Api.Features.Vehicles;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddSingleton<PasswordHasher<User>>();
builder.Services.AddSingleton<TokenProvider>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)
            ),
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero,
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.ForceThemeMode = ThemeMode.Dark;
        options.Theme = ScalarTheme.Moon;
    });
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapCreateLicense();
app.MapGetLicense();
app.MapDeleteLicense();
app.MapCreateUser();
app.MapGetUser();
app.MapDeleteUser();
app.MapCreateVehicle();
app.MapGetVehicle();
app.MapDeleteVehicle();
app.MapLoginUser();

app.Run();