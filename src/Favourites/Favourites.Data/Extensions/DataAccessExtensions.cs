using Microsoft.EntityFrameworkCore;

namespace Favourites.Data.Extensions;

internal static class DataAccessExtensions
{
    internal static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query,
        params string[] includes) where T : class
    {
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return query;
    }
}