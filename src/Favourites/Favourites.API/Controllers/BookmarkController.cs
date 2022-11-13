﻿using AutoMapper;
using Favourites.API.Models;
using Favourites.Data.Entities;
using Favourites.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Favourites.API.Controllers;

[ApiController]
[Route("api/bookmarks")]
public class BookmarkController : ControllerBase
{
    private readonly IBookmarkService _bookmarkService;
    private readonly IMapper _mapper;
    private readonly ILogger<BookmarkController> _logger;

    public BookmarkController(IBookmarkService bookmarkService, IMapper mapper, ILogger<BookmarkController> logger)
    {
        _bookmarkService = bookmarkService ?? throw new ArgumentNullException(nameof(bookmarkService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    private readonly IList<BookmarkDto> _bookmarks = new List<BookmarkDto>
    {
        new() { WebLink = "https://google.com", Name = "google", Tags = new[] { "it" } },
        new() { WebLink = "https://dndbeyond.com", Name = "dndbeyond" }
    };

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<BookmarkDto>>> GetBookmarks()
    {
        var bookmarkEntities = await _bookmarkService.GetAllBookmarksAsync();
        var result = _mapper.Map<IEnumerable<BookmarkDto>>(bookmarkEntities);
        return Ok(result);
    }

    [HttpGet("{name}", Name = "GetBookmark")]
    public ActionResult<BookmarkDto> GetBookmark(string name)
    {
        //TODO still needs to be changed to use data service
        var bookmark = _bookmarks.FirstOrDefault(x => x.Name.Equals(name));
        if (bookmark == null)
        {
            return NotFound();
        }

        return Ok(bookmark);
    }

    [HttpPost("")]
    public async Task<ActionResult<BookmarkDto>> CreateBookmark(BookmarkForCreationDto bookmark)
    {
        var bookmarkEntities = await _bookmarkService.GetAllBookmarksAsync();
        if (bookmarkEntities.Any(x => x.Name == bookmark.Name))
        {
            var existingBookmark = _bookmarks.First(x => x.Name == bookmark.Name);
            _logger.LogInformation("Bookmark \"{BookmarkName}\" already exists", bookmark.Name);
            return Conflict(existingBookmark);
        }

        try
        {
            var bookmarkEntity = _mapper.Map<Bookmark>(bookmark);
            await _bookmarkService.UpsertAsync(bookmarkEntity);
            return CreatedAtRoute("GetBookmark", new
            {
                bookmark.Name
            }, _mapper.Map<BookmarkDto>(bookmarkEntity));
        }
        catch (Exception ex)
        {
            _logger.LogCritical("An error happened while creating a bookmark {Bookmark} {Exception}", ex, bookmark);
            return StatusCode(500, "A problem happened while handling your request");
        }
    }

    [HttpPut("")]
    public ActionResult<BookmarkDto> UpdateBookmark(BookmarkForUpdateDto bookmark)
    {
        //TODO still needs to be changed to use data service
        if (_bookmarks.All(x => x.Name != bookmark.Name))
        {
            return NotFound();
        }

        var dataBookmark = _bookmarks.Single(x => x.Name == bookmark.Name);
        dataBookmark.Description = bookmark.Description;
        dataBookmark.WebLink = bookmark.WebLink;
        return NoContent();
    }

    [HttpDelete("{name}")]
    public async Task<ActionResult<BookmarkDto>> DeleteBookmark(string name)
    {
        var bookmarkEntities = await _bookmarkService.GetAllBookmarksAsync();
        if (bookmarkEntities.All(x => x.Name != name))
        {
            return NotFound();
        }

        try
        {
            var dataBookmark = bookmarkEntities.Single(x => x.Name == name);
            await _bookmarkService.DeleteAsync(dataBookmark);
            _logger.LogInformation("Bookmark {Name} was deleted", name);
        }
        catch (Exception ex)
        {
            _logger.LogCritical("An error happened while deleting a bookmark {Bookmark} {Exception}", ex, name);
            return StatusCode(500, "A problem happened while handling your request");
        }

        return NoContent();
    }
}