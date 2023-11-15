using System.ComponentModel.DataAnnotations;

namespace Favourites.Data.Entities;

public class Bookmark(string name, Uri webLink) : EntityBase(name)
{
    [Required]
    [Url]
    public Uri WebLink { get; set; } = webLink;

    [MaxLength(255)]
    public string? Description { get; set; }

    public ICollection<Tag>? Tags { get; set; }
}