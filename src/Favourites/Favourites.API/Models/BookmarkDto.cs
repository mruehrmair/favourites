using System.ComponentModel.DataAnnotations;

namespace Favourites.API.Models;

public record BookmarkDto
{
    public DateTime? ModificationDate { get; set; } 
    
    [Required]
    [Url]
    public string WebLink { get; set; } = null!;

    [Required]
    public string Name { get; init; } = null!;
    
    public string? Description { get; set; } = null;

    public string[] Tags { get; init; } = Array.Empty<string>();
    
}