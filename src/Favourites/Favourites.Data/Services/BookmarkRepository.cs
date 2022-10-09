using Favourites.Data.DbContexts;
using Favourites.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Favourites.Data.Services
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly BookmarkDbContext _dbContext;

        public BookmarkRepository(BookmarkDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        
        public Task CreateBookmark(Bookmark bookmark)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Bookmark>> GetBookmarks()
        {
            return await _dbContext.Bookmarks.ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetTags()
        {
            return await _dbContext.Tags.ToListAsync();
        }
    }
}
