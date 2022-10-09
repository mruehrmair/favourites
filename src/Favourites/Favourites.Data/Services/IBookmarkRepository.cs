using Favourites.Data.Entities;

namespace Favourites.Data.Services
{
    public interface IBookmarkRepository
    {
        public Task<IEnumerable<Bookmark>> GetBookmarks();
        
        public Task<IEnumerable<Tag>> GetTags();

        public Task CreateBookmark(Bookmark bookmark);
    }
}
