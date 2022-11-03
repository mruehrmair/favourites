using Microsoft.AspNetCore.Mvc;

namespace Favourites.API.Controllers;

[ApiController]
public class BookmarkController : ControllerBase
{
    [HttpGet("api/bookmarks")]
    public JsonResult GetBookmarks()
    {
        return new JsonResult(
            new List<object>
            {
                new { Weblink = "https://google.com", Description = "Google" },
                new { Weblink = "https://dndbeyond.com", Description = "DnD" }
            }
        );
    }
}