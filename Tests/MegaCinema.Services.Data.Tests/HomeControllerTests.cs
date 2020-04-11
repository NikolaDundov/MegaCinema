namespace MegaCinema.Services.Data.Tests
{
    using MegaCinema.Web.Controllers;
    using MegaCinema.Web.ViewModels.Movie;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;
    using MegaCinema.Services.Mapping;

    public class HomeControllerTests
    {
        [Fact]
        public void TestViewModelInIndexPage()
        {
            var mockService = new Mock<IMoviesService>();
            mockService.Setup(x => x.AllMovies<IndexMovieViewModel>()).Returns(new List<IndexMovieViewModel>
             {
                new IndexMovieViewModel { Id = 1 },
                new IndexMovieViewModel { Id = 2 },
                new IndexMovieViewModel { Id = 3 },
             });

            var controller = new HomeController(mockService.Object);
            var result = controller.Index();
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsType<IndexMovieViewModel>(viewResult.Model);
            var viewModel = viewResult.Model as List<IndexMovieViewModel>;
            var indexViewModel = new AllIndexMovieViewModel();
            indexViewModel.AllMovies = viewModel;
            Assert.Equal(3, indexViewModel.AllMovies.Count());
        }
    }
}
