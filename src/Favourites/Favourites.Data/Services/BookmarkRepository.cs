using Favourites.Data.DbContexts;
using Favourites.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Favourites.Data.Services
{
    public class BookmarkRepository : AbstractRepository<Bookmark>
    {
        private readonly TagRepository _tagRepo;

        public BookmarkRepository(DbContext dbContext) : base(dbContext)
        {
            _tagRepo = new TagRepository(dbContext);
        }

        public override async Task UpsertAsync(Bookmark entity)
        {
            if (entity.Tags != null)
            {
                var allTags = await _tagRepo.GetAllAsync();
                var updatedTagCollection = new List<Tag>();
                updatedTagCollection.AddRange(from tag in entity.Tags
                    let existingTag = allTags.FirstOrDefault(x => x.Name == tag.Name)
                    select existingTag ?? tag);
                entity.Tags = updatedTagCollection;
            }
            await base.UpsertAsync(entity);
        }
    }
}