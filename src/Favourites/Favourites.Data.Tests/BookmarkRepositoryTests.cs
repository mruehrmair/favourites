﻿using Favourites.Data.Services;

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
            var bookmarks = await sut.GetBookmarks();
            
            //Assert
            Assert.That(bookmarks.Count(), Is.EqualTo(expectedNumberOfSeededObjects));
        }

        [Test]
        public async Task GetTags_BaseSeeding_ReturnsCorrectNumberOfObjects()
        {
            //Arrange
            var sut = new BookmarkRepository(DbContext);
            const int expectedNumberOfSeededObjects = 3;

            //Act
            var tags = await sut.GetTags();

            //Assert
            Assert.That(tags.Count(), Is.EqualTo(expectedNumberOfSeededObjects));
        }

    }
}
