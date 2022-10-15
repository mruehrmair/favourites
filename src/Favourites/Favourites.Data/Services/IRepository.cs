using Favourites.Data.Entities;

namespace Favourites.Data.Services
{
    public interface IRepository<T> where T : EntityBase
    {
        public Task<ICollection<T>> GetAllAsync();

        public Task UpsertAsync(T entity);
    }
}
