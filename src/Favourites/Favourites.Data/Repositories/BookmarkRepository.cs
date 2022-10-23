using Favourites.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Favourites.Data.Repositories
{
    public class BookmarkRepository : AbstractRepository<Bookmark>, IBookmarkRepository
    {
        public BookmarkRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}