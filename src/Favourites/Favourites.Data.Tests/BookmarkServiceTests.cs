﻿using Favourites.Data.Entities;
using Favourites.Data.Services;

namespace Favourites.Data.Tests
{
    internal class BookmarkServiceTests : AbstractTest
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
            var sut = new BookmarkService(BookmarkRepository, TagRepository);
            const int expectedNumberOfSeededObjects = 2;
            const int expectedNumberOfTags = 4;

            //Act
            var bookmarks = await sut.GetAllBookmarksAsync();

            //Assert
            Assert.That(bookmarks, Has.Count.EqualTo(expectedNumberOfSeededObjects));
            Assert.That(bookmarks.SelectMany(x => x.Tags ?? new List<Tag>()).ToList(),
                Has.Count.EqualTo(expectedNumberOfTags));
        }

        [Test]
        [TestCase("donjon", 1)]
        [TestCase("bin", 2)]
        public async Task GetBookmarks_SearchTextInBaseSeeding_ReturnsCorrectNumberOfObjects(string searchString,
            int expectedResults)
        {
            //Arrange
            var sut = new BookmarkService(BookmarkRepository, TagRepository);

            //Act
            var bookmarks = await sut.GetAllBookmarksAsync(searchString);

            //Assert
            Assert.That(bookmarks, Has.Count.EqualTo(expectedResults));
        }

        [Test]
        public async Task UpsertBookmark_NewBookmarkWithoutTags_BookmarkAdded()
        {
            //Arrange
            var sut = new BookmarkService(BookmarkRepository, TagRepository);
            var newBookmark = new Bookmark("opencms", new Uri("https://www.opencms.org/"));
            const int expectedNumberOfSeededObjects = 3;

            //Act
            await sut.UpsertAsync(newBookmark);
            var bookmarks = await sut.GetAllBookmarksAsync();

            //Assert
            Assert.That(bookmarks, Has.Count.EqualTo(expectedNumberOfSeededObjects));
        }

        [Test]
        public async Task UpsertBookmark_ExistingBookmarkWithoutTags_BookmarkUpdated()
        {
            //Arrange
            const string description = "RPG Randomizer website";
            const string name = "donjon";
            var sut = new BookmarkService(BookmarkRepository, TagRepository);
            var existingBookmark = new Bookmark(name, new Uri("https://donjon.bin.sh/"))
            {
                Description = description
            };
            const int expectedNumberOfSeededObjects = 2;

            //Act
            await sut.UpsertAsync(existingBookmark);
            var bookmarks = await sut.GetAllBookmarksAsync();

            //Assert
            Assert.That(bookmarks, Has.Count.EqualTo(expectedNumberOfSeededObjects));
            Assert.That(bookmarks.FirstOrDefault(x => x.Name == name)?.Description, Is.EqualTo(description));
        }

        [Test]
        public async Task UpsertBookmark_ExistingBookmarkWithNewTag_BookmarkUpdated()
        {
            //Arrange
            const string description = "RPG Randomizer website";
            const string name = "donjon";
            var sut = new BookmarkService(BookmarkRepository, TagRepository);
            var existingBookmark = new Bookmark(name, new Uri("https://donjon.bin.sh/"))
            {
                Description = description,
                Tags = new List<Tag> { new("rpg"), new("donjon") }
            };
            const int expectedNumberOfSeededObjects = 2;
            const int expectedNumberOfTags = 4;

            //Act
            await sut.UpsertAsync(existingBookmark);
            var bookmarks = await sut.GetAllBookmarksAsync();
            var allTags = await sut.GetAllTagsAsync();
            
            //Assert
            Assert.That(bookmarks, Has.Count.EqualTo(expectedNumberOfSeededObjects));
            Assert.That(bookmarks.FirstOrDefault(x => x.Name == name)?.Description, Is.EqualTo(description));
            Assert.That(allTags,
                Has.Count.EqualTo(expectedNumberOfTags));
        }

        [Test]
        public async Task UpsertBookmark_NewBookmarkWithTags_BookmarkAdded()
        {
            //Arrange
            var sut = new BookmarkService(BookmarkRepository, TagRepository);
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
            var bookmarks = await sut.GetAllBookmarksAsync();
            var tags = await sut.GetAllTagsAsync();

            //Assert
            Assert.That(bookmarks, Has.Count.EqualTo(expectedNumberOfBookmarks));
            Assert.That(tags, Has.Count.EqualTo(expectedNumberOfTotalTags));
            Assert.That(tags.FirstOrDefault(x => x.Name == tagName)?.Bookmarks,
                Has.Count.EqualTo(expectedNumberOfBookmarks));
        }

        [Test]
        public async Task DeleteBookmark_SeededBookmark_BookmarkRemoved()
        {
            //Arrange
            var sut = new BookmarkService(BookmarkRepository, TagRepository);
            var newBookmark = new Bookmark("donjon", new Uri("https://donjon.bin.sh/"));
            const int expectedNumberOfObjects = 1;

            //Act
            await sut.DeleteAsync(newBookmark);
            var bookmarks = await sut.GetAllBookmarksAsync();

            //Assert
            Assert.That(bookmarks, Has.Count.EqualTo(expectedNumberOfObjects));
        }

        [Test]
        public async Task GetTags_BaseSeeding_ReturnsCorrectNumberOfObjects()
        {
            //Arrange
            var sut = new BookmarkService(BookmarkRepository, TagRepository);
            const int expectedNumberOfSeededObjects = 3;

            //Act
            var tags = await sut.GetAllTagsAsync();

            //Assert
            Assert.That(tags, Has.Count.EqualTo(expectedNumberOfSeededObjects));
        }
    }
}