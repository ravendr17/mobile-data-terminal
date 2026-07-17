using FluentValidation;
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.MapCreateLicense();
app.MapGetLicense();
app.MapDeleteLicense();
app.MapCreateUser();
app.MapGetUser();

app.Run();