using Favourites.Data.DbContexts;
using Favourites.Data.Entities;
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
                var rpgTag = new Tag("rpg");
                DbContext.AddRange(
                    new Bookmark("donjon", new Uri("https://donjon.bin.sh/"))
                    {
                        Description = "RPG Randomizer",
                        Tags = new[] { rpgTag },
                    },
                    new Bookmark("roll20APIScripts", new Uri("https://github.com/RobinKuiper/Roll20APIScripts"))
                    {
                        Tags = new[] { rpgTag, new Tag("roll20"), new Tag("github") }
                    });
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
