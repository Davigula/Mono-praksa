using Recipe.API.Service.Common;
using Microsoft.AspNetCore.Mvc;
using Recipe.Model;
using Recipe.Common;


namespace Recipe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IService<WeatherForecast> ServiceObject;
        
        private readonly Sorting Sorting;
        private readonly Pagging Pagging;
        
        public WeatherForecastController(IService<WeatherForecast> service)
        {
            // Ruèno kreiranje instance WeatherForecastService bez DI
            //var repository = new WeatherForecastRepository();
            //_weatherForecastService = new WeatherForecastService(repository);

            ServiceObject = service;
            
        }

        [HttpGet("GetWeatherForecast")]
        public IActionResult Get(string orderBy = "id", string sortOrder  ="ASC", int rpp = 10, int pageNumber = 1, string summary = "", int temperature = 17)
        {
            try
            {
                Sorting sorting = new Sorting
                {
                    OrderBy = orderBy,
                    SortOrder = sortOrder
                };
                Pagging pagging = new Pagging
                {
                    RecordPerPage = rpp,
                    PageNumber = pageNumber

                };
                AddFilter filter = new AddFilter
                {
                    Summary = summary,
                    TemperatureC = temperature

                };
                var forecasts = ServiceObject.Get(sorting, pagging, filter);
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
                var forecast = ServiceObject.GetById(id);
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
                    ServiceObject.Post(weather);
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
                    ServiceObject.Put(id, updatedWeather);
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
                    ServiceObject.Delete(id);
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
