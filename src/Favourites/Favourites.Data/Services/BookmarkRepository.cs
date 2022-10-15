using Favourites.Data.DbContexts;
using Favourites.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Favourites.Data.Services
{
    public class BookmarkRepository : AbstractRepository<Bookmark>
    {
        public BookmarkRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
