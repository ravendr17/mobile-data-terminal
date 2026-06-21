using Backend.Data;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var dbConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(dbConnectionStr)
           .UseSnakeCaseNamingConvention()
);

builder.Services.AddScoped<LicenseService>();
builder.Services.AddScoped<VehicleService>();

builder.Services.AddControllers();

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



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();