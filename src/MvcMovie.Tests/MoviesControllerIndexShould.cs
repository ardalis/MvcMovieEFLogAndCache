using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Moq;
using MvcMovie.Controllers;
using MvcMovie.Core.Interfaces;
using NUnit.Framework;

namespace MvcMovie.Tests
{
    [TestFixture]
    public class MoviesControllerIndexShould
    {
        private class FakeMovieRepository : IMovieRepository
        {
            public List<string> GenreList { private get; set; }
            public bool WasListMoviesCalled { get; private set; }
            public IEnumerable<string> ListGenres()
            {
                return GenreList;
            }

            public IEnumerable<Models.Movie> ListMovies(string movieGenre, string searchString)
            {
                WasListMoviesCalled = true;
                return null;
            }
        }

        [Test]
        public void ReturnGenresInViewBag()
        {
            var expectedGenres = new List<string>()
            {
                "Action", "Horror", "Comedy"
            };
            var fakeRepo = new FakeMovieRepository()
            {
                GenreList = expectedGenres
            };
            var controller = new MoviesController(fakeRepo);

            var result = controller.Index("foo", "bar") as ViewResult;

            SelectList genreSelectList = result.ViewBag.movieGenre as SelectList;
            foreach (var genreItem in genreSelectList)
            {
                CollectionAssert.Contains(expectedGenres, genreItem.Text);
            }
            Assert.That(fakeRepo.WasListMoviesCalled);
        }

        [Test]
        public void ReturnGenresInViewBagWithMock()
        {
            var expectedGenres = new List<string>()
            {
                "Action", "Horror", "Comedy"
            };
            string testGenreSearch = "testGenreSearch";
            string testTitleSearch = "testTitleSearch";
            var mockRepo = new Mock<IMovieRepository>();
            mockRepo.Setup(m => m.ListGenres())
                .Returns(expectedGenres);
            
            var controller = new MoviesController(mockRepo.Object);

            var result = controller.Index(testGenreSearch, testTitleSearch) as ViewResult;

            SelectList genreSelectList = result.ViewBag.movieGenre as SelectList;
            foreach (var genreItem in genreSelectList)
            {
                CollectionAssert.Contains(expectedGenres, genreItem.Text);
            }
            mockRepo.Verify(r => r.ListMovies(testGenreSearch, testTitleSearch));
        }
    }
}
