using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using static Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;



namespace Recipe.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        

        
        private const string CONNECTION_STRING = "Host=localhost:5432;" +
            "Username=postgres;" +
            "Password=1234;" +
            "Database=postgres";

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

        const string TABLE_NAME = "\"WeatherForecasts\"";

        // select po id za to služi private 


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
        [HttpGet("GetAnotherForecast/{id}")]
        //[Route("getbyss/{ss}")]
        public IActionResult GetById(int id)
        {
            
            
            
            //List<WeatherForecast> wheaterList = list.Where(p => p.Summary == ss).ToList();

            //if (wheaterList == null || !wheaterList.Any()){
                
            //    return BadRequest("Element not found");
                
            //}
            //else
            //{
            //    return Ok(wheaterList);
            //}
            
        }

        [HttpPost]
        public IActionResult Post(WeatherForecast weather )
        {

            string commandText = $"INSERT INTO {TABLE_NAME} (id, date, temperaturec, summary) VALUES (@id, @date, @tempC, @summ) ";
            try
            {
                using (var connection = new NpgsqlConnection(CONNECTION_STRING))
                {
                    using var command = new NpgsqlCommand(commandText, connection);
                        
                    connection.Open();

                    command.Parameters.AddWithValue("id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                    command.Parameters.AddWithValue("date", weather.Date);
                    command.Parameters.AddWithValue("tempC", weather.TemperatureC);
                    command.Parameters.AddWithValue("summ", NpgsqlTypes.NpgsqlDbType.Varchar, weather.Summary);

                    var numberOfCommits = command.ExecuteNonQuery();
                    connection.Close();

                    if (numberOfCommits > 0)
                    {

                        return Ok("ALl good.");
                    }
                    else
                    {
                        return BadRequest("Didn't enter commit");
                    }

                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
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

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            string commandText = $"DELETE FROM {TABLE_NAME} WHERE ID=(@id)";

            try
            { 
                using (var connection = new NpgsqlConnection(CONNECTION_STRING)){ 
                    using var command = new NpgsqlCommand(commandText, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("id", id);
                    var numberOfCommits = command.ExecuteNonQuery();

                    if (numberOfCommits < 1)
                    {
                        return BadRequest("Data isnt deleted");
                    }else{

                        return Ok("Data deleted successfully!");
                    }
                
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            
            
            

        }



    }
}
