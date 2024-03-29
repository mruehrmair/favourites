﻿using Favourites.Data.Entities;

namespace Favourites.Data.Repositories;

public interface IRepository<T> where T : EntityBase
{
    public Task<ICollection<T>> GetAllAsync(Func<T, bool>? search = null, bool isNoTracking = false, params string[] includes);

    public Task<T?> GetAsync(T entity);

    public Task UpdateAsync(T existingEntity, T newEntity);

    public Task CreateAsync(T entity);

    public Task DeleteAsync(T entity);
}