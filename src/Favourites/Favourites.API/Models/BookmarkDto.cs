namespace Favourites.API.Models;

public record BookmarkDto(string WebLink, string Name)
{
    public string? Description { get; init; } = null;

    public string[] Tags { get; init; } = Array.Empty<string>();
}