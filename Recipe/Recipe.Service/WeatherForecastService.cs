using Recipe.API.Service.Common;
using Recipe.API.Repository;
using System;
using System.Collections.Generic;
using Recipe.API.Repository.Common;
using Recipe.Model;
using Recipe.Service;

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

        //public override bool Update(Guid id, WeatherForecast entity)
        //{
        //    if(e)
        //}
    }
}

