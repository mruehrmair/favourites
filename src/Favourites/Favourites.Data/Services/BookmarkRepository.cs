using Favourites.Data.DbContexts;
using Favourites.Data.Entities;

namespace Favourites.Data.Services
{
    public class BookmarkRepository : AbstractRepository<Bookmark>
    {
        public BookmarkRepository(BookmarkDbContext dbContext) : base(dbContext)
        {
        }
    }
}
