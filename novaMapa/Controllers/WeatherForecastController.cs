using Recipe.API.Service;
using Recipe.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Recipe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherForecastService _weatherForecastService;

        public WeatherForecastController()
        {
            // Ruèno kreiranje instance WeatherForecastService bez DI
            var repository = new WeatherForecastRepository();
            _weatherForecastService = new WeatherForecastService(repository);
        }

        [HttpGet("GetWeatherForecast")]
        public IActionResult Get()
        {
            try
            {
                var forecasts = _weatherForecastService.Get();
                return Ok(forecasts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAnotherForecast/{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var forecast = _weatherForecastService.GetById(id);
                if (forecast == null)
                {
                    return NotFound("Forecast not found with the provided ID.");
                }
                return Ok(forecast);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(WeatherForecast weather)
        {
            try
            {
                if (weather != null)
                {
                    _weatherForecastService.Post(weather);
                    return Ok("Weather forecast added successfully.");
                }
                return BadRequest("Failed to add weather forecast.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, WeatherForecast updatedWeather)
        {
            try
            {
                if (updatedWeather != null)
                {
                    _weatherForecastService.Put(id, updatedWeather);
                    return Ok("Weather forecast updated successfully.");
                }
                return NotFound("Weather forecast not found with the provided ID.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    _weatherForecastService.Delete(id);
                    return Ok("Weather forecast deleted successfully.");
                }
                return NotFound("Weather forecast not found with the provided ID.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
