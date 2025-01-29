using System.Text;
using Npgsql;
using Recipe.API.Repository.Common;
using Recipe.Common;
using Recipe.Model;

namespace Recipe.API.Repository
{
    public class WeatherForecastRepository : IRepository<WeatherForecast>
    {
        protected readonly string ConnectionString = "Host=localhost:5432;Username=postgres;Password=1234;Database=postgres";
            
        protected readonly string TableName = "\"WeatherForecasts\"";

        
        

        private void ApplyPagging(Pagging pagging, StringBuilder query, NpgsqlCommand command)
        {
            pagging.PageNumber = pagging.PageNumber > 0 ? pagging.PageNumber : 1;
            pagging.RecordPerPage = pagging.RecordPerPage > 0 ? pagging.PageNumber : 10;

            query.Append($" LIMIT @rpp OFFSET ((@offset - 1) * @rpp)"); ;
            command.Parameters.AddWithValue("offset", pagging.PageNumber);
            command.Parameters.AddWithValue("rpp", pagging.RecordPerPage);
        }

        private void ApplyFilter(Filter filter, StringBuilder query, NpgsqlCommand command)
        {
            query.Append(" WHERE 1=1");
            if (!string.IsNullOrWhiteSpace(filter.Summary))
            {
                query.Append(" AND summary = @summary");
                command.Parameters.AddWithValue("summary", filter.Summary);
            }
            if (filter.TemperatureC != 0)
            {
                query.Append(" AND temperaturec = @tempc");
                command.Parameters.AddWithValue("tempc", filter.TemperatureC);
            }
        }

        public  List<WeatherForecast> Get(Sorting sorting, Pagging pagging, Filter filter)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            StringBuilder query = new StringBuilder($"SELECT id, date, temperaturec, summary FROM \"WeatherForecasts\"");
            if (filter == null)
            {
                throw new Exception("Nema filtera");
            }
            //ApplyFilter(filter, query, command);

            if (sorting != null)
            {
                sorting.OrderBy = string.IsNullOrWhiteSpace(sorting.OrderBy) ? "id" : sorting.OrderBy;
                sorting.SortOrder = string.IsNullOrWhiteSpace(sorting.SortOrder) ? "ASC" : "DESC";
                query.Append($" ORDER BY {sorting.OrderBy ?? "id "} {sorting.SortOrder ?? "ASC"}");
            }

            if (pagging != null)
            {
                if (sorting == null)
                {
                    throw new Exception("Nema sortinga");
                }
                //ApplyPagging(pagging, query, command);
            }


            var weatherList = new List<WeatherForecast>();
            Console.WriteLine(query.ToString());
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

        public  WeatherForecast GetById(Guid id)
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

        public  bool Insert(WeatherForecast weather)
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

        public  bool Update(Guid id, WeatherForecast updatedWeather)
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

        public  bool Delete(Guid id)
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