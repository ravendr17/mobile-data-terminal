using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MobileDataTerminal.Api.Data;
using MobileDataTerminal.Api.Exceptions;
using MobileDataTerminal.Api.Features.Licenses;
using MobileDataTerminal.Api.Features.Users;
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

app.MapCreateLicense();
app.MapGetLicense();
app.MapDeleteLicense();
app.MapCreateUser();
app.MapGetUser();
app.MapDeleteUser();

app.Run();