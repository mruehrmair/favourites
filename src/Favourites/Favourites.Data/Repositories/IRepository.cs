using Favourites.Data.Entities;

namespace Favourites.Data.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        public Task<ICollection<T>> GetAllAsync();

        public Task UpsertAsync(T entity);
    }
}
