using Favourites.Data.Entities;
using Favourites.Data.Services;

namespace Favourites.Data.Tests
{

    internal class BookmarkRepositoryTests : AbstractTest
    {
        [Test]
        public void DbContextIsNotNull()
        {
            Assert.That(DbContext, Is.Not.Null);
        }

        [Test]
        public async Task GetBookmarks_BaseSeeding_ReturnsCorrectNumberOfObjects()
        {
            //Arrange
            var sut = new BookmarkRepository(DbContext);
            const int expectedNumberOfSeededObjects = 2;

            //Act
            var bookmarks = await sut.GetAllAsync();
            
            //Assert
            Assert.That(bookmarks.Count(), Is.EqualTo(expectedNumberOfSeededObjects));
        }

        [Test]
        public async Task UpsertBookmark_NewBookmark_BookmarkAdded()
        {
            //Arrange
            var sut = new BookmarkRepository(DbContext);
            var newBookmark = new Bookmark("OpenCms", new Uri("http://www.opencms.org/"));
            const int expectedNumberOfSeededObjects = 3;

            //Act
            await sut.UpsertAsync(newBookmark);
            var bookmarks = await sut.GetAllAsync();

            //Assert
            Assert.That(bookmarks.Count(), Is.EqualTo(expectedNumberOfSeededObjects));
        }


        [Test]
        public async Task GetTags_BaseSeeding_ReturnsCorrectNumberOfObjects()
        {
            //Arrange
            var sut = new TagRepository(DbContext);
            const int expectedNumberOfSeededObjects = 3;

            //Act
            var tags = await sut.GetAllAsync();

            //Assert
            Assert.That(tags.Count(), Is.EqualTo(expectedNumberOfSeededObjects));
        }
    }
}
