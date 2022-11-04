using System.ComponentModel.DataAnnotations;

namespace Favourites.Data.Entities;

public class Bookmark : EntityBase
{
    [Required]
    [Url]
    public Uri WebLink { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    public ICollection<Tag>? Tags { get; set; }

    public Bookmark(string name, Uri webLink) : base(name)
    {
        WebLink = webLink;
    }
}