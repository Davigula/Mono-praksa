using Recipe.API.Repository.Common;
using Recipe.Common;

namespace Recipe.API.Repository
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        protected readonly string ConnectionString;
        protected readonly string TableName;

        protected BaseRepository(string connectionString, string tableName)
        {
            ConnectionString = connectionString;
            TableName = tableName;
        }

        public abstract List<T> Get(Sorting sorting, Pagging pagging, AddFilter filter);
        public abstract T GetById(Guid id);
        public abstract bool Insert(T entity);
        public abstract bool Update(Guid id, T entity);
        public abstract bool Delete(Guid id);
    }
}
