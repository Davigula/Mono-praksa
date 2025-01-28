using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Npgsql;
using Recipe.API.Repository.Common;
using Recipe.Common;
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

        public void ApplyPagging(Pagging pagging, StringBuilder query, NpgsqlCommand command)
        {

            pagging.PageNumber = pagging.PageNumber > 0 ? pagging.PageNumber : 1;
            pagging.RecordPerPage = pagging.RecordPerPage > 0 ? pagging.PageNumber : 10 ;

            query.Append($" OFFSET @offset LIMIT @rpp ");                ;
            command.Parameters.AddWithValue("offset", (pagging.PageNumber - 1) * pagging.RecordPerPage);
            command.Parameters.AddWithValue("rpp", pagging.RecordPerPage);
        }

        public void ApplyFilter(AddFilter filter, StringBuilder query, NpgsqlCommand command)
        {
            query.Append(" WHERE 1=1 ");
            if(!string.IsNullOrWhiteSpace(filter.Summary))
            { 
                query.Append(" AND summary = @summary ");
                command.Parameters.AddWithValue("summary", filter.Summary);
            }
            if (filter.TemperatureC != 0)
            {
                query.Append(" and temperaturec = @tempc ");
                command.Parameters.AddWithValue("tempc", filter.TemperatureC);
            }

                       
        }



        public override List<WeatherForecast> Get(Sorting sorting, Pagging pagging, AddFilter filter)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            //string commandText = $"SELECT id, date, temperaturec, summary FROM {TableName}";
            StringBuilder query = new StringBuilder($"SELECT id, date, temperaturec, summary FROM {TableName} ");
            //if (sorting == null) throw new Exception("There is no sorting");
            //if (pagging == null) throw new Exception("There is no pagging");
            if (filter == null)
            {
                throw new Exception("Nema filtera");

            }
            ApplyFilter(filter, query, command);
            
            if (sorting != null)
            {
                sorting.OrderBy = string.IsNullOrWhiteSpace(sorting.OrderBy) ? "id" : sorting.OrderBy;
                sorting.SortOrder = string.IsNullOrWhiteSpace(sorting.SortOrder) ? "ASC" : "DESC";
                query.Append($" ORDER BY {sorting.OrderBy ?? "id "}  {sorting.SortOrder ?? "ASC "}  ");
            }
            
            if (pagging != null)
            {
                if(sorting == null)
                {
                    throw new Exception("Nema sortinga");
                }
                ApplyPagging(pagging, query, command);
            }
            




            //myStringBuilder.Append($"OFFSET {(Pagging.PageNumber-1)*Pagging.RecordPerPage} ROWS FETCH NEXT {Pagging.RecordPerPage} ROWS ONLY");
            //myStringBuilder.Append( $"WHERE temperaturec = {temperatureC}");

            var weatherList = new List<WeatherForecast>();

            using (var connection = new NpgsqlConnection(ConnectionString))
            using (command = new NpgsqlCommand(query.ToString(), connection))
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

