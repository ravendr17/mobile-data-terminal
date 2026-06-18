var builder = WebApplication.CreateBuilder(args);

var dbConnStr = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();