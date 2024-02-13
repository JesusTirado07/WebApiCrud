using Microsoft.AspNetCore.Mvc;

namespace WebAPIProducto.controllers;

[ApiController]
[Route("[controller]")]

public class weatherforecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<weatherforecastController> _logger;

    public weatherforecastController(ILogger<weatherforecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet (Name = "GetWeatherForecast")]

    public IEnumerable<WeatherForecast> Get ()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}