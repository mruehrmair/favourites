using Favourites.Data.Entities;
using Favourites.Data.Repositories;

namespace Favourites.Data.Services;

public class BookmarkService : IBookmarkService
{
    private readonly IBookmarkRepository _bookmarkRepository;
    private readonly ITagRepository _tagRepository;

    public BookmarkService(IBookmarkRepository bookmarkRepository, ITagRepository tagRepository)
    {
        _bookmarkRepository = bookmarkRepository;
        _tagRepository = tagRepository;
    }
    
    public async Task UpsertAsync(Bookmark entity)
    {
        if (entity.Tags != null)
        {
            var allTags = await _tagRepository.GetAllAsync();
            var updatedTagCollection = new List<Tag>();
            updatedTagCollection.AddRange(from tag in entity.Tags
                let existingTag = allTags.FirstOrDefault(x => x.Name == tag.Name)
                select existingTag ?? tag);
            entity.Tags = updatedTagCollection;
        }

        if (await _bookmarkRepository.GetAsync(entity) != null)
        {
            await _bookmarkRepository.UpdateAsync(entity);
        }
        else
        {
            await _bookmarkRepository.CreateAsync(entity);
        }
    }

    public async Task DeleteAsync(Bookmark entity)
    {
        await _bookmarkRepository.DeleteAsync(entity);
    }

    public async Task<ICollection<Bookmark>> GetAllBookmarksAsync()
    {
        return await _bookmarkRepository.GetAllAsync();
    }

    public async Task<ICollection<Tag>> GetAllTagsAsync()
    {
        return await _tagRepository.GetAllAsync();
    }
}