using AutoMapper;
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
      
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<BookmarkDto>>> GetBookmarks()
    {
        var bookmarkEntities = await _bookmarkService.GetAllBookmarksAsync();
        var result = _mapper.Map<IEnumerable<BookmarkDto>>(bookmarkEntities);
        return Ok(result);
    }

    [HttpGet("{name}", Name = "GetBookmark")]
    public async Task<ActionResult<BookmarkDto>> GetBookmark(string name)
    {
        var bookmark = (await _bookmarkService.GetAllBookmarksAsync()).FirstOrDefault(x => x.Name.Equals(name));
        if (bookmark == null)
        {
            return NotFound();
        }
        var result = _mapper.Map<BookmarkDto>(bookmark);
        return Ok(bookmark);
    }

    [HttpPost("")]
    public async Task<ActionResult<BookmarkDto>> CreateBookmark(BookmarkForCreationDto bookmark)
    {
        var bookmarkEntities = await _bookmarkService.GetAllBookmarksAsync();
        var bookmarkName = bookmark.Name.ToLower();
        if (bookmarkEntities.Any(x => x.Name == bookmarkName))
        {
            var existingBookmark = bookmarkEntities.First(x => x.Name == bookmarkName);
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

    [HttpPost("")]
    public async Task<ActionResult<BookmarkDto>> CreateBookmarks(IEnumerable<BookmarkForCreationDto> bookmarks)
    {
        throw new NotImplementedException();
    }

    [HttpPut("")]
    public async Task<ActionResult<BookmarkDto>> UpdateBookmark(BookmarkForUpdateDto bookmark)
    {
        var bookmarkEntities = await _bookmarkService.GetAllBookmarksAsync();
        if (bookmarkEntities.All(x => x.Name != bookmark.Name))
        {
            return NotFound();
        }
        try
        {
            var bookmarkEntity = _mapper.Map<Bookmark>(bookmark);
            await _bookmarkService.UpsertAsync(bookmarkEntity);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogCritical("An error happened while creating a bookmark {Bookmark} {Exception}", ex, bookmark);
            return StatusCode(500, "A problem happened while handling your request");
        }
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