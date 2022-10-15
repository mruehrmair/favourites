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
            Assert.That(bookmarks, Has.Count.EqualTo(expectedNumberOfSeededObjects));
        }

        [Test]
        public async Task UpsertBookmark_NewBookmarkWithoutTags_BookmarkAdded()
        {
            //Arrange
            var sut = new BookmarkRepository(DbContext);
            var newBookmark = new Bookmark("opencms", new Uri("https://www.opencms.org/"));
            const int expectedNumberOfSeededObjects = 3;

            //Act
            await sut.UpsertAsync(newBookmark);
            var bookmarks = await sut.GetAllAsync();

            //Assert
            Assert.That(bookmarks, Has.Count.EqualTo(expectedNumberOfSeededObjects));
        }

        [Test]
        public async Task UpsertBookmark_ExistingBookmarkWithoutTags_BookmarkUpdated()
        {
            //Arrange
            const string description = "RPG Randomizer website";
            const string name = "donjon";
            var sut = new BookmarkRepository(DbContext);
            var existingBookmark = new Bookmark(name, new Uri("https://donjon.bin.sh/"))
            {
                Description = description
            };
            const int expectedNumberOfSeededObjects = 2;

            //Act
            await sut.UpsertAsync(existingBookmark);
            var bookmarks = await sut.GetAllAsync();

            //Assert
            Assert.That(bookmarks, Has.Count.EqualTo(expectedNumberOfSeededObjects));
            Assert.That(bookmarks.FirstOrDefault(x => x.Name == name)?.Description, Is.EqualTo(description));
        }

        [Test]
        public async Task UpsertBookmark_NewBookmarkWithTags_BookmarkAdded()
        {
            //Arrange
            var sut = new BookmarkRepository(DbContext);
            var tagRepo = new TagRepository(DbContext);
            const string tagName = "rpg";
            var newBookmark = new Bookmark("dndbeyond", new Uri("https://www.dndbeyond.com/"))
            {
                Description = "Official D&D online toolset",
                Tags = new List<Tag> { new(tagName), new("d&d") }
            };
            const int expectedNumberOfBookmarks = 3;
            const int expectedNumberOfTotalTags = 4;
            
            //Act
            await sut.UpsertAsync(newBookmark);
            var bookmarks = await sut.GetAllAsync();
            var tags = await tagRepo.GetAllAsync();
            
            //Assert
            Assert.That(bookmarks, Has.Count.EqualTo(expectedNumberOfBookmarks));
            Assert.That(tags, Has.Count.EqualTo(expectedNumberOfTotalTags));
            Assert.That(tags.FirstOrDefault(x => x.Name == tagName)?.Bookmarks,
                Has.Count.EqualTo(expectedNumberOfBookmarks));
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
            Assert.That(tags, Has.Count.EqualTo(expectedNumberOfSeededObjects));
        }
    }
}