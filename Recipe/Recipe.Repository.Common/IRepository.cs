using Recipe.Common;

namespace Recipe.API.Repository.Common
{
    
    public interface IRepository<T>
    {
        List<T> Get(Sorting sorting, Pagging pagging, Filter filter);

        T GetById(Guid id);

        bool Insert(T entity);

        bool Update(Guid id, T entity);

        bool Delete(Guid id);
    }
}