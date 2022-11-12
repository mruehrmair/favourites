using Favourites.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Favourites.Data.DbContexts
{
    public class BookmarkDbContext : DbContext
    {
        // ReSharper disable once UnusedMember.Global
        public DbSet<Bookmark> Bookmarks { get; set; } = null!;

        // ReSharper disable once UnusedMember.Global
        public DbSet<Tag> Tags { get; set; } = null!;

        public BookmarkDbContext()
        {
        }
        
        public BookmarkDbContext(DbContextOptions<BookmarkDbContext> options) : base(options)
        {
        }
    }
}
