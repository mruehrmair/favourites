namespace Favourites.Data.Entities;

public class Tag(string name) : EntityBase(name)
{
    public ICollection<Bookmark>? Bookmarks { get; set; }
}