using Favourites.Data.Entities;

namespace Favourites.Data.Services;

public interface IBookmarkService
{
    Task UpsertAsync(Bookmark entity);
    
    Task DeleteAsync(Bookmark entity);
    
    Task<IReadOnlyCollection<Bookmark>> GetAllBookmarksAsync(string? search = null);
    
    Task<IReadOnlyCollection<Tag>> GetAllTagsAsync();
}