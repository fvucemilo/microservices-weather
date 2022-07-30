using CloudWeather.Temperature.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TemperatureDbContext>(
  opts =>  {
    opts.EnableSensitiveDataLogging();
    opts.EnableDetailedErrors();
    opts.UseNpgsql(builder.Configuration.GetConnectionString("AppDb"));
  }, ServiceLifetime.Transient
);

var app = builder.Build();

app.MapGet("/observation/{zip}", async (string zip, [FromQuery] int? days, TemperatureDbContext db) => {
  if (days == null || days < 1 || days > 30)
  {
    return Results.BadRequest("Please provide a 'days' query parameter between 1 and 30");
  }
  var startData = DateTime.UtcNow - TimeSpan.FromDays(days.Value);
  var results = await db.Temperature
    .Where(percip => percip.ZipCode == zip && percip.CreatedOn > startData)
    .ToListAsync();

  return Results.Ok(results);
});

app.MapPost("/observation", async (Temperature temperature, TemperatureDbContext db) => {
  temperature.CreatedOn = temperature.CreatedOn.ToUniversalTime();
  await db.AddAsync(temperature);
  await db.SaveChangesAsync();
});

app.Run();
