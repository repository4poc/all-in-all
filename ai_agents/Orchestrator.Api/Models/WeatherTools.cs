using System;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;

public class WeatherTools
{
    private readonly HttpClient _httpClient = new HttpClient();

    [Description("Get the current weather for a given location.")]
    public async Task<string> GetWeather(
        [Description("The location to get the weather for.")] string location)
    {
        try
        {
            // Step 1: Convert location name to coordinates
            Console.WriteLine($"Fetching weather for location : {Uri.EscapeDataString(location)}");
            var geoUrl =
                $"https://geocoding-api.open-meteo.com/v1/search?name={Uri.EscapeDataString(location)}&count=1";

            Console.WriteLine($"Geo URL: {geoUrl}");

            var geoResponse = await _httpClient.GetStringAsync(geoUrl);

            var geoJSON = JsonDocument.Parse(geoResponse);

            Console.WriteLine($"Geo response: {geoResponse}");

            if (geoJSON.RootElement.GetProperty("results").GetArrayLength() == 0)
            {
                return $"Could not find location '{location}'.";
            }

            var first = geoJSON.RootElement.GetProperty("results")[0];
            double latitude = first.GetProperty("latitude").GetDouble();
            double longitude = first.GetProperty("longitude").GetDouble();
            string resolvedName = first.GetProperty("name").GetString()!;
            string country = first.GetProperty("country").GetString()!;

            // Step 2: Get current weather
            var weatherUrl1 =
                $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,weather_code,wind_speed_10m";

            var weatherUrl =
                $"https://api.open-meteo.com/v1/forecast?latitude={latitude.ToString(CultureInfo.InvariantCulture)}" +
                $"&longitude={longitude.ToString(CultureInfo.InvariantCulture)}" +
                "&current=temperature_2m,weather_code,wind_speed_10m";

            Console.WriteLine($"Weather URL: {weatherUrl}");
            var weatherResponse = await _httpClient.GetStringAsync(weatherUrl);

            using var weatherDoc = JsonDocument.Parse(weatherResponse);

            var current = weatherDoc.RootElement.GetProperty("current");

            double temperature = current.GetProperty("temperature_2m").GetDouble();
            double windSpeed = current.GetProperty("wind_speed_10m").GetDouble();
            int weatherCode = current.GetProperty("weather_code").GetInt32();

            return $"{resolvedName}, {country}: {WeatherCodeToDescription(weatherCode)}, " +
                   $"{temperature}°C, Wind {windSpeed} km/h";
        }
        catch (Exception ex)
        {
            return $"Failed to retrieve weather: {ex.Message}";
        }
    }

    private string WeatherCodeToDescription(int code) => code switch
    {
        0 => "Clear sky",
        1 => "Mainly clear",
        2 => "Partly cloudy",
        3 => "Overcast",
        45 or 48 => "Fog",
        51 or 53 or 55 => "Drizzle",
        61 or 63 or 65 => "Rain",
        71 or 73 or 75 => "Snow",
        80 or 81 or 82 => "Rain showers",
        95 => "Thunderstorm",
        _ => "Unknown"
    };
}