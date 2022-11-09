using Favourites.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Favourites.API.Controllers;

[ApiController]
[Route("api/bookmarks")]
public class BookmarkController : ControllerBase
{
    private readonly IList<BookmarkDto> _bookmarks = new List<BookmarkDto>
    {
        new() { WebLink = "https://google.com", Name = "google", Tags = new[] { "it" } },
        new() { WebLink = "https://dndbeyond.com", Name = "dndbeyond" }
    };

    [HttpGet("")]
    public ActionResult<IEnumerable<BookmarkDto>> GetBookmarks()
    {
        return Ok(_bookmarks);
    }

    [HttpGet("{name}", Name = "GetBookmark")]
    public ActionResult<BookmarkDto> GetBookmark(string name)
    {
        var bookmark = _bookmarks.FirstOrDefault(x => x.Name.Equals(name));
        if (bookmark == null)
        {
            return NotFound();
        }

        return Ok(bookmark);
    }

    [HttpPost("")]
    public ActionResult<BookmarkDto> CreateBookmark(BookmarkForCreationDto bookmark)
    {
        var dataBookmark = new BookmarkDto { WebLink = bookmark.WebLink, Name = bookmark.Name };

        _bookmarks.Add(dataBookmark);

        return CreatedAtRoute("GetBookmark", new
        {
            bookmark.Name
        }, dataBookmark);
    }

    [HttpPut("")]
    public ActionResult<BookmarkDto> UpdateBookmark(BookmarkForUpdateDto bookmark)
    {
        if (_bookmarks.All(x => x.Name != bookmark.Name))
        {
            return NotFound();
        }

        var dataBookmark = _bookmarks.Single(x => x.Name == bookmark.Name);
        dataBookmark.Description = bookmark.Description;
        dataBookmark.WebLink = bookmark.WebLink;
        return NoContent();
    }
}