using Favourites.Data.DbContexts;
using Favourites.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Favourites.Data.Services
{
    public class TagRepository : AbstractRepository<Tag>
    {
        public TagRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
