using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly WeatherService _weatherService;

    public WeatherController(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("{city}")]
    public async Task<IActionResult> GetWeather(string city)
    {
        var result = await _weatherService.GetWeatherAsync(city);

        if (string.IsNullOrEmpty(result))
        {
            return BadRequest(new { error = "Weather data not found." });
        }

        return Ok(new { message = result });
    }
}
