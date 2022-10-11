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

        public async Task UpsertAsync(T entity)
        {
            var set = DbContext.Set<T>();
            var dbEntity = await set.FindAsync(entity.Name);
            entity.ModificationDate = DateTime.UtcNow;
            if (dbEntity != null)
            {
                //Update
                set.Update(entity);
            }
            else
            {
                //Insert               
                set.Add(entity);
            }
            await DbContext.SaveChangesAsync();
        }
    }
}
