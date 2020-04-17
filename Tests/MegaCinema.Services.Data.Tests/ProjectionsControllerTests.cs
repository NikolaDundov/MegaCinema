namespace MegaCinema.Services.Data.Tests
{
    using MegaCinema.Web.Controllers;
    using MegaCinema.Web.ViewModels.Projection;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xunit;

    public class ProjectionsControllerTests
    {
        [Fact]
        public void TestViewModelForAvailableMovies()
        {
            var mockProjectionService = new Mock<IProjectionsService>();
            var mockMovieService = new Mock<IMoviesService>();
            var mockCinemaService = new Mock<ICinemaService>();
            mockProjectionService.Setup(x => x.AllProjections<ProjectionViewModel>())
                .Returns(this.ProjectionsViewModelData());

            var controller = new ProjectionsController(
                mockProjectionService.Object,
                mockMovieService.Object,
                mockCinemaService.Object);

            var result = controller.All();
            Assert.IsAssignableFrom<IActionResult>(result);

            //var viewResult = result as IActionResult;
            //Assert.IsType<AllProjectionsViewModel>(viewResult.Model);
            //var viewModel = viewResult.Model as AllProjectionsViewModel;
            //Assert.Equal(2, viewModel.AllProjections.Count);
        }

        public List<ProjectionViewModel> ProjectionsViewModelData()
        {
            return new List<ProjectionViewModel>
            {
                new ProjectionViewModel
                {
                    CinemaId = 1,
                    HallId = 5,
                    MovieId = 15,
                    Id = 1,
                    StartTime = new DateTime(2020, 04, 26, 15, 30, 00),
                    Type = MegaCinema.Data.Models.ProjectionType._2D,
                },
                new ProjectionViewModel
                {
                    CinemaId = 1,
                    HallId = 5,
                    MovieId = 15,
                    Id = 1,
                    StartTime = new DateTime(2020, 04, 26, 15, 30, 00),
                    Type = MegaCinema.Data.Models.ProjectionType._2D,
                },
            };
        }
    }
}
