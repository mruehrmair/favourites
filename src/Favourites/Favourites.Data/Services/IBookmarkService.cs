using Favourites.Data.Entities;

namespace Favourites.Data.Services;

public interface IBookmarkService
{
    Task UpsertAsync(Bookmark entity);

    Task<ICollection<Bookmark>> GetAllBookmarksAsync();
    
    Task<ICollection<Tag>> GetAllTagsAsync();
}