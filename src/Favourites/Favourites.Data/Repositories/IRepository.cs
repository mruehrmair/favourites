using Favourites.Data.Entities;

namespace Favourites.Data.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        public Task<ICollection<T>> GetAllAsync();

        public Task<T?> GetAsync(T entity);
        
        public Task UpdateAsync(T entity);

        public Task CreateAsync(T entity);

        public Task DeleteAsync(T entity);
    }
}
