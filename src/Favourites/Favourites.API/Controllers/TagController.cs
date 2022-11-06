using Microsoft.AspNetCore.Mvc;

namespace Favourites.API.Controllers;

[ApiController]
public class TagController : ControllerBase
{
    private readonly IList<string> _tags = new List<string>
    {
        ".net",
        "rpg"
    };
    
    [HttpGet("api/tags")]
    public ActionResult<IEnumerable<string>> GetTags()
    {
        return Ok(_tags);
    }
}