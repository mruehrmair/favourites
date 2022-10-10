using Favourites.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Favourites.Data.Services
{
    public abstract class AbstractRepository<T> : IRepository<T> where T : EntityBase
    {
        protected readonly DbContext DbContext;

        public AbstractRepository(DbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
               
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public Task UpsertAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
