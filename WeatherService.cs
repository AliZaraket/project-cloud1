using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    // Constructor to inject HttpClient and Configuration
    public WeatherService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["OPENWEATHER_API_KEY"]; // Retrieve API key from configuration
    }

    // Method to fetch weather data for a given city
    public async Task<string> GetWeatherAsync(string city)
    {
        // Build the OpenWeather API URL
        string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";

        try
        {
            // Send a GET request to the API
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                // Read and parse the JSON response
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var weatherData = JObject.Parse(jsonResponse);

                // Extract temperature information from the JSON response
                string temperature = weatherData["main"]["temp"].ToString();
                string description = weatherData["weather"][0]["description"].ToString();
                return $"Current temperature in {city} is {temperature}°C with {description}.";
            }
            else
            {
                // Handle the case where the API call fails
                string errorMessage = $"Error: Unable to fetch weather data. Status Code: {response.StatusCode}";
                string errorDetails = await response.Content.ReadAsStringAsync();
                return $"{errorMessage} Details: {errorDetails}";
            }
        }
        catch (HttpRequestException ex)
        {
            // Handle HTTP request issues (e.g., network errors, API server not reachable)
            return $"Request Error: {ex.Message}";
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors
            return $"Unexpected Error: {ex.Message}";
        }
    }
}
