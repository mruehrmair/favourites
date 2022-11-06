using System.ComponentModel.DataAnnotations;

namespace Favourites.API.Models;

public record BookmarkForCreationDto
{
    [Required]
    [Url]
    public string WebLink { get; init; } = null!;

    [Required]
    public string Name { get; init; } = null!;

    [MaxLength(255)]
    public string? Description { get; init; } = null;

    public string[] Tags { get; init; } = Array.Empty<string>();
}