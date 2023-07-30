using Favourites.Data.DbContexts;
using Favourites.Data.Entities;

namespace Favourites.Data.Repositories;

public class BookmarkRepository : AbstractRepository<Bookmark>, IBookmarkRepository
{
    public BookmarkRepository(BookmarkDbContext dbContext) : base(dbContext)
    {}
}