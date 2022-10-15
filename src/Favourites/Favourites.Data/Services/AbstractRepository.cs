using Favourites.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Favourites.Data.Services
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

        public virtual async Task UpsertAsync(T entity)
        {
            var set = _dbContext.Set<T>();
            var dbEntity = await set.FindAsync(entity.Name);
            entity.ModificationDate = DateTime.UtcNow;
            if (dbEntity != null)
            {
                //Update
                _dbContext.Entry(dbEntity).State = EntityState.Detached;
                set.Update(entity);
            }
            else
            {
                //Insert               
                set.Add(entity);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
