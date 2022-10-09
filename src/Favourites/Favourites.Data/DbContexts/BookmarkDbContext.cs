using Favourites.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Favourites.Data.DbContexts
{
    public class BookmarkDbContext : DbContext
    {
        public DbSet<Bookmark> Bookmarks { get; set; } = null!;

        public DbSet<Tag> Tags { get; set; } = null!;

        public BookmarkDbContext(DbContextOptions<BookmarkDbContext> options) : base(options)
        {
            //Connection String Sqlite: "Data Source=Bookmarks.db"
        }
    }
}
