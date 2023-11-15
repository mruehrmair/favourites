using Favourites.Data.Entities;
using Favourites.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Favourites.Data.Repositories;

public abstract class AbstractRepository<T>(DbContext dbContext) : IRepository<T>
    where T : EntityBase
{
    private readonly DbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task<ICollection<T>> GetAllAsync(Func<T, bool>? search = null, bool isNoTracking = false, params string[] includes)
    {
        var set = _dbContext.Set<T>();
        var query = set.AsQueryable();
        if (includes.Length > 0)
        {
            query = set.IncludeMultiple(includes);
        }

        if (isNoTracking)
        {
            query = query.AsNoTracking();
        }
        if (search != null)
        {
            return query.AsEnumerable().Where(search).ToList();
        }
        return await query.ToListAsync();
    }

    public virtual async Task<T?> GetAsync(T entity)
    {
        var set = _dbContext.Set<T>();
        var dbEntity = await set.FindAsync(entity.Name);
        return dbEntity ?? null;
    }

    /// <summary>
    /// Updating / replacing an existing entity
    /// </summary>
    /// <param name="existingEntity">entity currently in db</param>
    /// <param name="newEntity">entity with properties to update</param>
    public virtual async Task UpdateAsync(T existingEntity, T newEntity)
    {
        _dbContext.Entry(existingEntity).State = EntityState.Detached;
        _dbContext.Set<T>().Update(newEntity);
        newEntity.ModificationDate = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task CreateAsync(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        entity.ModificationDate = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        var dbEntity = await GetAsync(entity);
        if (dbEntity != null)
        {
            _dbContext.Remove(dbEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}