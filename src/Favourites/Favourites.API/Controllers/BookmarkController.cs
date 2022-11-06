using Favourites.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Favourites.API.Controllers;

[ApiController]
[Route("api/bookmarks")]
public class BookmarkController : ControllerBase
{
    private readonly IList<BookmarkDto> _bookmarks = new List<BookmarkDto>
    {
        new("https://google.com", "google") { Tags = new[] { "it" } },
        new("https://dndbeyond.com", "dndbeyond")
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
        var dataBookmark = new BookmarkDto(bookmark.WebLink, bookmark.Name);

        _bookmarks.Add(dataBookmark);

        return CreatedAtRoute("GetBookmark", new
        {
            bookmark.Name
        }, dataBookmark);
    }
}