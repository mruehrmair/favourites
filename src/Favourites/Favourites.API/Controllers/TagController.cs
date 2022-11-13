using Favourites.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Favourites.API.Controllers;

[ApiController]
[Route("api/tags")]
public class TagController : ControllerBase
{
    private readonly IBookmarkService _bookmarkService;
    private readonly ILogger<TagController> _logger;

    public TagController(IBookmarkService bookmarkService, ILogger<TagController> logger)
    {
        _bookmarkService = bookmarkService ?? throw new ArgumentNullException(nameof(bookmarkService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<string>>> GetTags()
    {
        var result = (await _bookmarkService.GetAllTagsAsync()).Select(x => x.Name);
        return Ok(result);
    }
    
    [HttpDelete("{name}")]
    public async Task<ActionResult> DeleteTag(string name)
    {
        //TODO should tags that are in use be deletable?
        var tagEntities = await _bookmarkService.GetAllTagsAsync();
        if (tagEntities.All(x => x.Name != name))
        {
            return NotFound();
        }

        try
        {
            var tagEntity = tagEntities.Single(x => x.Name == name);
            await _bookmarkService.DeleteTagAsync(tagEntity);
            _logger.LogInformation("Tag {Name} was deleted", name);
        }
        catch (Exception ex)
        {
            _logger.LogCritical("An error happened while deleting a tag {Tag} {Exception}", ex, name);
            return StatusCode(500, "A problem happened while handling your request");
        }

        return NoContent();
    }
}