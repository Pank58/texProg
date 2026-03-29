using Magazine.Core.Services;
using Magazine.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Magazine.WebApi.Data; 

var builder = WebApplication.CreateBuilder(args);

// поддержка контроллеров
builder.Services.AddControllers();

// регистрация сервисов ".
// поменялся тип
builder.Services.AddSingleton<IProductService, ProductService>();

// регистрация Базы Данных 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
     options.UseSqlite("Data Source=magazine.db"));


builder.Services.AddOpenApi();

var app = builder.Build();

// Настройка конвейера запросов
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.MapControllers();


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
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
