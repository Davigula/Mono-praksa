using System;
using System.Collections.Generic;
using System.Data;
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
            string commandText = $"SELECT id, date, temperaturec, summary FROM {TABLE_NAME}";
            try { 
                using(var connection = new NpgsqlConnection(CONNECTION_STRING)) {
                    using var command = new NpgsqlCommand(commandText, connection);
                    connection.Open();
                    using(var reader = command.ExecuteReader()) {
                        var weatherList = new List<WeatherForecast>();
                        while (reader.Read()) {
                            var weather = new WeatherForecast
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("id")),
                                TemperatureC = reader.GetInt32(reader.GetOrdinal("temperaturec")),
                                Summary = reader.GetString(reader.GetOrdinal("summary"))
                                //Date = reader.GetString(ToString( reader.GetOrdinal("id"))),
                            };
                            weatherList.Add(weather);
                        }
                        if(weatherList.Count > 0)
                        {
                            return Ok(weatherList);
                        }
                        else
                        {
                            return BadRequest("No records found");
                        }
                    
                    }
                
                }
            
            }catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAnotherForecast/{id}")]
        //[Route("getbyss/{ss}")]
        public IActionResult GetById(Guid id)
        {
            string commandText = $"SELECT id, date, temperaturec, summary FROM {TABLE_NAME} WHERE id=@id";
            try
            {
                using (var connection = new NpgsqlConnection(CONNECTION_STRING))
                {
                    using var command = new NpgsqlCommand(commandText, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("id", id);

                    using (var Reader = command.ExecuteReader()){
                        if (Reader.Read()) {
                            WeatherForecast weather = new WeatherForecast{
                                Id = Reader.GetGuid(Reader.GetOrdinal("id")),
                                //Date = Reader.GetOrdinal("date")
                                //Date = Reader.GetString(Reader.GetOrdinal("date").ToString()),
                                TemperatureC = Reader.GetInt32(Reader.GetOrdinal("temperaturec")),
                                Summary = Reader.GetString(Reader.GetOrdinal("summary"))


                            };
                            return Ok(weather);
                        }
                        else
                        {
                            return BadRequest("Nema takvog elementa!");
                        }
                    }
                }
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
                
            
            
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

        //[HttpPut("{id}")]
        //public IActionResult Put(Guid id, WeatherForecast updatedWeather)
        //{
        //    var getResult = GetById(id);
        //    if(getResult)

        //}

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {

            var getResult = GetById(id);
            if(getResult is NotFoundResult)
            {
                return NotFound("Not found with that ID");
            }

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
                return BadRequest("jaoooo");
            }
            
            
            
            

        }



    }
}
