using Favourites.Data.DbContexts;
using Favourites.Data.Entities;

namespace Favourites.Data.Services
{
    public class TagRepository : AbstractRepository<Tag>
    {
        public TagRepository(BookmarkDbContext dbContext) : base(dbContext)
        {
        }
    }
}
