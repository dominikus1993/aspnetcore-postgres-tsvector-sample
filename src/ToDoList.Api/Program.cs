using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Api.Infrastructure;
using ToDoList.Api.Infrastructure.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddInfrastructure();
var app = builder.Build();
await app.MigrateDatabaseAsync();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/todos", async (string query, [FromServices]ToDoDbContext db, CancellationToken cancellationToken) =>
    {
        var res = await db.ToDos.AsSplitQuery().Where(x => x.SearchVector.Matches(query)).Select(x => new { x.Id, x.Name, x.Items }).ToListAsync(cancellationToken: cancellationToken);
        if (res is null or {Count: 0})
        {
            return Results.NoContent();
        }

        return Results.Ok(res);
    })
    .WithName("SearchToDos")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
