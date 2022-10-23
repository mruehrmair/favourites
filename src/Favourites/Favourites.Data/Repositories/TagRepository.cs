using Favourites.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Favourites.Data.Repositories
{
    public class TagRepository : AbstractRepository<Tag>, ITagRepository
    {
        public TagRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
