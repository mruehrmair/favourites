using Favourites.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Favourites.API.Controllers;

[ApiController]
public class BookmarkController : ControllerBase
{
    private readonly IList<BookmarkDto> _bookmarks = new List<BookmarkDto>
    {
        new("https://google.com", "google"),
        new("https://dndbeyond.com", "dndbeyond")
    };

    [HttpGet("api/bookmarks")]
    public ActionResult<IEnumerable<BookmarkDto>> GetBookmarks()
    {
        return Ok(_bookmarks);
    }
    
    [HttpGet("api/bookmarks/{name}")]
    public ActionResult<BookmarkDto> GetBookmark(string name)
    {
        var bookmark = _bookmarks.FirstOrDefault(x => x.Name.Equals(name));
        if (bookmark == null)
        {
            return NotFound();
        }
        return Ok(bookmark);
    }
}