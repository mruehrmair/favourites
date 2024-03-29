﻿using Favourites.Data.Entities;
using Favourites.Data.Repositories;

namespace Favourites.Data.Services;

public class BookmarkService : IBookmarkService
{
    private readonly IBookmarkRepository _bookmarkRepository;
    private readonly ITagRepository _tagRepository;

    private const string Tags = $"{nameof(Bookmark.Tags)}";
    private const string Bookmarks = $"{nameof(Tag.Bookmarks)}";

    public BookmarkService(IBookmarkRepository bookmarkRepository, ITagRepository tagRepository)
    {
        _bookmarkRepository = bookmarkRepository;
        _tagRepository = tagRepository;
    }

    public async Task UpsertAsync(Bookmark entity)
    {
        if (entity.Tags != null)
        {
            entity.Tags = (await UpdateTags(entity.Tags)).ToList();
            foreach (var tag in entity.Tags)
            {
                tag.Bookmarks ??= new List<Bookmark> { entity };
            }
        }
        
        var existingEntity = await _bookmarkRepository.GetAsync(entity);
        if (existingEntity != null)
        {
            await _bookmarkRepository.DeleteAsync(existingEntity);
        }
        await _bookmarkRepository.CreateAsync(entity);
    }

    public async Task UpsertAllAsync(IEnumerable<Bookmark> entities)
    {
        foreach (var entity in entities)
        {
            await UpsertAsync(entity);
        }
    }

    public async Task DeleteAsync(Bookmark entity)
    {
        await _bookmarkRepository.DeleteAsync(entity);
    }

    public async Task<IReadOnlyCollection<Bookmark>> GetAllBookmarksAsync(string? search = null)
    {
        if (search == null)
            return (IReadOnlyCollection<Bookmark>)await _bookmarkRepository.GetAllAsync(null, true, Tags);

        bool SearchDelegate(Bookmark b) => b.Name.Contains(search) || b.WebLink.AbsoluteUri.Contains(search) ||
                                           b.Description != null && b.Description.Contains(search);

        return (IReadOnlyCollection<Bookmark>)await _bookmarkRepository.GetAllAsync(SearchDelegate, true, Tags);
    }

    public async Task<IReadOnlyCollection<Tag>> GetAllTagsAsync()
    {
        return (IReadOnlyCollection<Tag>)await _tagRepository.GetAllAsync(null, false, Bookmarks);
    }

    public async Task DeleteTagAsync(Tag entity)
    {
        await _tagRepository.DeleteAsync(entity);
    }
    
    private async Task<IEnumerable<Tag>> UpdateTags(IEnumerable<Tag> entityTags)
    {
        var allTags = await GetAllTagsAsync();
        var tags = entityTags.ToList();
        var updatedTagCollection = new List<Tag>();
        updatedTagCollection.AddRange(from tag in tags
            let existingTag = allTags.FirstOrDefault(x => x.Name == tag.Name)
            select existingTag ?? tag);
        return updatedTagCollection;
    }
}