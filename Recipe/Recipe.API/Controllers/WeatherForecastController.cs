using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace Recipe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //public IEnumerable<WeatherForecast> list = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //{
        //    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //    TemperatureC = Random.Shared.Next(-20, 55),
        //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //})
        //    .ToArray();
        private static List<WeatherForecast> list = 
            [];



        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetWeatherForecast")]
        public IActionResult Get()
        {
            if(!list.Any()){
                
                return BadRequest("List is empty");
            }
            else
            {
                return Ok(list);
            }
        }
        [HttpGet("GetAnotherForecast/{ss}")]
        //[Route("getbyss/{ss}")]
        public IActionResult GetDavss(string ss)
        {
            List<WeatherForecast> wheaterList = list.Where(p => p.Summary == ss).ToList();

            if (wheaterList == null || !wheaterList.Any()){
                
                return BadRequest("Element not found");
                
            }
            else
            {
                return Ok(wheaterList);
            }
            
        }

        [HttpPost("{summ}")]
        public IActionResult Post(string summ )
        {

            
            if (summ is string )
            {
                WeatherForecast newly = new WeatherForecast
                {

                    TemperatureC = 0,
                    Summary = summ
                };
                list.Add(newly);
                return Ok("Element successfully added");
            }
            else
            {
                return BadRequest("There is no such object");

            }
        }

        [HttpPut("{tempC}")]
        public IActionResult Put(int tempC, string newlySum= "Today")
        {
            WeatherForecast weather = list.FirstOrDefault(p => p.TemperatureC == tempC);
            
            if(weather != null)
            {
                weather.Summary = "Not so bad";
                return Ok("Element successfully changed"); 
            }else
            {
                return BadRequest("There is no such object");

            }

        }

        [HttpDelete("{tempC}")]
        public IActionResult Delete(int tempC, string newlySum = "Today")
        {
            WeatherForecast weather = list.FirstOrDefault(p => p.TemperatureC == tempC);

            if (weather != null)
            {
                list.Remove(weather);
                return Ok("Element uspješno izbrisan");
            }
            else
            {
                return BadRequest("There is no such object");

            }

        }



    }
}
