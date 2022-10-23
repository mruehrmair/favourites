using Favourites.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Favourites.Data.Repositories
{
    public abstract class AbstractRepository<T> : IRepository<T> where T : EntityBase
    {
        private readonly DbContext _dbContext;

        protected AbstractRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T?> GetAsync(T entity)
        {
            var set = _dbContext.Set<T>();
            var dbEntity = await set.FindAsync(entity.Name);
            return dbEntity ?? null;
        }
        
        public virtual async Task UpdateAsync(T entity)
        {
            var set = _dbContext.Set<T>();
            var dbEntity = await set.FindAsync(entity.Name);
            entity.ModificationDate = DateTime.UtcNow;
            if (dbEntity != null)
            {
                _dbContext.Entry(dbEntity).State = EntityState.Detached;
                set.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public virtual async Task CreateAsync(T entity)
        {
            var set = _dbContext.Set<T>();
            entity.ModificationDate = DateTime.UtcNow;
            set.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            var set = _dbContext.Set<T>();
            var dbEntity = await set.FindAsync(entity.Name);
            if (dbEntity != null)
            {
                _dbContext.Remove(dbEntity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
