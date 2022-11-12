using Favourites.Data.DbContexts;
using Favourites.Data.Entities;

namespace Favourites.Data.Repositories;

public class TagRepository : AbstractRepository<Tag>, ITagRepository
{
    public TagRepository(BookmarkDbContext dbContext) : base(dbContext)
    {
    }
}