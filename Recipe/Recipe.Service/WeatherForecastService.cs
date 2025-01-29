using Recipe.API.Repository.Common;
using Recipe.Common;
using Recipe.Model;
using Recipe.Service;

namespace Recipe.API.Service
{
    public class WeatherForecastService : BaseService<WeatherForecast>
    {
        private readonly IRepository<WeatherForecast> _repository;
        private readonly Sorting Sorting;
        private readonly Pagging Pagging;

        public WeatherForecastService(IRepository<WeatherForecast> repository) : base(repository)
        {
            _repository = repository;
        }

        public override bool Post(WeatherForecast entity)
        {
            if (string.IsNullOrEmpty(entity.Summary))
            {
                throw new ArgumentException("Summary cannot be empty");
            }

            return base.Post(entity);
        }

        public override bool Delete(Guid id)
        {
            var entity = _repository.GetById(id);
            if(entity != null)
            {
                throw new Exception("No entity with that id");
            }
            return base.Delete(id);
        }

    }
}