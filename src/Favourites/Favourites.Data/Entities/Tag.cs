namespace Favourites.Data.Entities;

public class Tag : EntityBase
{
    public ICollection<Bookmark>? Bookmarks { get; set; }

    public Tag(string name) : base(name)
    {
    }
}