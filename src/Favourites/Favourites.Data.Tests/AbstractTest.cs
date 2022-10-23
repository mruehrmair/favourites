using Favourites.Data.DbContexts;
using Favourites.Data.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Favourites.Data.Tests
{
    [TestFixture]
    internal abstract class AbstractTest
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private SqliteConnection _connection = null!;

        protected BookmarkDbContext DbContext = null!;
        protected BookmarkRepository BookmarkRepository = null!;
        protected TagRepository TagRepository = null!;

        [SetUp]
        public void BaseSetUp()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<BookmarkDbContext>()
                    .UseSqlite(_connection)
                    .Options;
            DbContext = new BookmarkDbContext(options);

            if (DbContext.Database.EnsureCreated())
            {
                DbContext.AddRange(DataSeed.GetBookmarkData());
                DbContext.SaveChanges();
            }

            BookmarkRepository = new BookmarkRepository(DbContext);
            TagRepository = new TagRepository(DbContext);
        }

        [TearDown]
        public void BaseTearDown()
        {
            _connection.Close();
        }
    }
}
