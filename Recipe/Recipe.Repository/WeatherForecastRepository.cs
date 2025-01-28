using System;
using System.Collections.Generic;
using Npgsql;
using Recipe.API.Repository.Common;
using Recipe.Model;

namespace Recipe.API.Repository
{
    public class WeatherForecastRepository : BaseRepository<WeatherForecast>
    {
        public WeatherForecastRepository() : base(
            "Host=localhost:5432;Username=postgres;Password=1234;Database=postgres",
            "\"WeatherForecasts\"")
        {
        }

        public override List<WeatherForecast> Get()
        {
            string commandText = $"SELECT id, date, temperaturec, summary FROM {TableName}";
            var weatherList = new List<WeatherForecast>();

            using (var connection = new NpgsqlConnection(ConnectionString))
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                connection.Open();
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var weather = new WeatherForecast
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("id")),
                        TemperatureC = reader.GetInt32(reader.GetOrdinal("temperaturec")),
                        Summary = reader.GetString(reader.GetOrdinal("summary"))
                    };
                    weatherList.Add(weather);
                }
            }
            return weatherList;
        }

        public override WeatherForecast GetById(Guid id)
        {
            string commandText = $"SELECT id, date, temperaturec, summary FROM {TableName} WHERE id=@id";
            using (var connection = new NpgsqlConnection(ConnectionString))
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("id", id);
                using var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new WeatherForecast
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("id")),
                        TemperatureC = reader.GetInt32(reader.GetOrdinal("temperaturec")),
                        Summary = reader.GetString(reader.GetOrdinal("summary"))
                    };
                }
            }
            return null;
        }

        public override bool Insert(WeatherForecast weather)
        {
            string commandText = $"INSERT INTO {TableName} (id, date, temperaturec, summary) VALUES (@id, @date, @tempC, @summ)";
            using (var connection = new NpgsqlConnection(ConnectionString))
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("id", Guid.NewGuid());
                command.Parameters.AddWithValue("date", weather.Date);
                command.Parameters.AddWithValue("tempC", weather.TemperatureC);
                command.Parameters.AddWithValue("summ", weather.Summary);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public override bool Update(Guid id, WeatherForecast updatedWeather)
        {
            string commandText = $"UPDATE {TableName} SET temperaturec = @tempC, summary = @summ WHERE id=@id";
            using (var connection = new NpgsqlConnection(ConnectionString))
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("tempC", updatedWeather.TemperatureC);
                command.Parameters.AddWithValue("summ", updatedWeather.Summary);

                return command.ExecuteNonQuery() > 0;
            }
        }

        public override bool Delete(Guid id)
        {
            string commandText = $"DELETE FROM {TableName} WHERE id=@id";
            using (var connection = new NpgsqlConnection(ConnectionString))
            using (var command = new NpgsqlCommand(commandText, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("id", id);

                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}

