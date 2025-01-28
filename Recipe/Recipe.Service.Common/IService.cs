

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

    
}
