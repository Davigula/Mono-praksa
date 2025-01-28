using Recipe.API.Repository.Common;
using Recipe.API.Service.Common;
using Recipe.Common;

namespace Recipe.Service
{
    public abstract class BaseService<T> : IService<T>
    {
        private readonly IRepository<T> _repository;
        

        protected BaseService(IRepository<T> repository)
        {
            _repository = repository;
            
        }
        
        
        public virtual List<T> Get(Sorting sorting, Pagging pagging, AddFilter filter)
        {
            return _repository.Get(sorting, pagging, filter);
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
