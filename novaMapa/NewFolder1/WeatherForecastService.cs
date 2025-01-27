using Recipe.API.Service.Common;
using Recipe.API.Repository;
using System;
using System.Collections.Generic;
using Recipe.API.Repository.Common;

namespace Recipe.API.Service
{
    public class WeatherForecastService : BaseService<WeatherForecast>
    {
        private readonly IRepository<WeatherForecast> _repository;

        public WeatherForecastService(IRepository<WeatherForecast> repository) : base(repository)
        {
            _repository = repository;
        }

        // Dodatna logika specifična za WeatherForecast, ako je potrebna
        public override bool Post(WeatherForecast entity)
        {
            // Možeš dodati dodatnu validaciju prije poziva u bazu
            if (string.IsNullOrEmpty(entity.Summary))
            {
                throw new ArgumentException("Summary cannot be empty");
            }

            return base.Post(entity);
        }
    }
}



//using System;
//using System.Collections.Generic;
//using Recipe.API.Controllers;
//using Recipe.API.Repository;
////using Recipe.API.Service;
////using Recipe.API.Service;

//namespace Recipe.API.Services
//{
//    public class WeatherForecastService
//    {
//        private readonly WeatherForecastRepository _repository;

//        public WeatherForecastService(WeatherForecastRepository repository)
//        {
//            _repository = repository;
//        }

//        public IEnumerable<WeatherForecast> Get()
//        {
//            return _repository.Get();
//        }

//        public WeatherForecast GetById(Guid id)
//        {
//            var weatherForecast = _repository.GetById(id);

//            if (weatherForecast == null)
//                throw new KeyNotFoundException("Weather forecast not found!");

//            return weatherForecast;
//        }

//        public void Post(WeatherForecast weatherForecast)
//        {
//            weatherForecast.Id = Guid.NewGuid();
//            _repository.Insert(weatherForecast);
//        }

//        public void Put(Guid id, WeatherForecast updatedWeather)
//        {
//            var existingWeather = _repository.GetById(id);

//            if (existingWeather == null)
//                throw new KeyNotFoundException("Weather forecast not found!");

//            existingWeather.TemperatureC = updatedWeather.TemperatureC;
//            existingWeather.Summary = updatedWeather.Summary;

//            _repository.Update(id, existingWeather);
//        }

//        public void Delete(Guid id)
//        {
//            var existingWeather = _repository.GetById(id);

//            if (existingWeather == null)
//                throw new KeyNotFoundException("Weather forecast not found!");

//            _repository.Delete(id);
//        }
//    }
//}
