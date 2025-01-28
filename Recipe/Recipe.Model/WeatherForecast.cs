<<<<<<< HEAD
namespace Recipe.Model
{
    public class WeatherForecast
    {
        public Guid Id { get; set;  }
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public string? Summary { get; set; }

        //public WeatherForecast(int tempC, string summ) { TemperatureC = tempC; Summary = summ; }
    }
}
=======
namespace Recipe.Model
{
    public class WeatherForecast
    {
        public Guid Id { get; set;  }
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public string? Summary { get; set; }

        //public WeatherForecast(int tempC, string summ) { TemperatureC = tempC; Summary = summ; }
    }
}
>>>>>>> ee6675f (Nadograđen program i dodate nove funkcionalnosti)
