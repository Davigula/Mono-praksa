using Recipe.API.Repository.Common;

namespace Recipe.API.Service.Common
{
    public interface IService<T>
    {
        List<T> Get();
        T GetById(Guid id);
        bool Post(T entity);
        bool Put(Guid id, T entity);
        bool Delete(Guid id);
    }

    public abstract class BaseService<T> : IService<T>
    {
        private readonly IRepository<T> _repository;

        protected BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual List<T> Get()
        {
            return _repository.Get();
        }

        public virtual T GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public virtual bool Post(T entity)
        {
            return _repository.Insert(entity);
        }

        public virtual bool Put(Guid id, T entity)
        {
            return _repository.Update(id, entity);
        }

        public virtual bool Delete(Guid id)
        {
            return _repository.Delete(id);
        }
    }
}
