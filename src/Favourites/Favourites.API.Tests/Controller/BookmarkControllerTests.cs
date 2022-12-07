using AutoMapper;
using Favourites.API.Controllers;
using Favourites.API.Models;
using Favourites.API.Profiles;
using Favourites.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Favourites.API.Tests.Controller
{
    internal class BookmarkControllerTests : AbstractControllerTest
    {
        protected BookmarkController Sut = null!;

        [SetUp]
        public void Setup()
        {
            var bookmarkProfile = new BookmarkProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(bookmarkProfile));
            var mapper = new Mapper(configuration);

            var bookmarkService = Substitute.For<Data.Services.IBookmarkService>();
            var bookmarks = new List<Bookmark> { new Bookmark("dndbeyond", new Uri("https://dndbeyond.com")) };
            bookmarkService.GetAllBookmarksAsync().Returns( bookmarks );
            var logger = Substitute.For<ILogger<BookmarkController>>();
            Sut = new BookmarkController(bookmarkService, mapper, logger);
        }

        [Test]
        public async Task GetBookmarksTest()
        {
            //Act
            var result = await Sut.GetBookmarks();

            //Assert
            var okObjectResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult);
            var models = okObjectResult.Value as IEnumerable<BookmarkDto>;
            Assert.IsNotNull(models);
            Assert.That(models.FirstOrDefault()?.Name, Is.EqualTo("dndbeyond"));
            Assert.That(models.Count(), Is.EqualTo(1));
        }
    }
}
