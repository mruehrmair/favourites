using Favourites.Data.DbContexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Favourites.Data.Tests
{
    [TestFixture]
    internal abstract class AbstractTest
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private SqliteConnection _connection;

        protected BookmarkDbContext DbContext;

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
        }

        [TearDown]
        public void BaseTearDown()
        {
            _connection.Close();
        }
    }
}
