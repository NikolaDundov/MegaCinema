namespace MegaCinema.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MegaCinema.Data;
    using MegaCinema.Services.Data;
    using MegaCinema.Web.ViewModels.Projection;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ProjectionsController : BaseController
    {
        private readonly IProjectionsService projectionsService;
        private readonly IMoviesService moviesService;
        private readonly ICinemaService cinemaService;

        public ProjectionsController(
            IProjectionsService projectionsService,
            IMoviesService moviesService,
            ICinemaService cinemaService)
        {
            this.projectionsService = projectionsService;
            this.moviesService = moviesService;
            this.cinemaService = cinemaService;
        }

        public IActionResult All()
        {
            var viewModel = new AllProjectionsViewModel
            {
                AllProjections = this.projectionsService.AllProjections<ProjectionViewModel>().ToList(),
            };

            return this.View(viewModel);
        }

        public IActionResult ByCinemaForToday(string cinemaName)
        {
            var viewModel = new AllProjectionsViewModel
            {
                AllProjections = this.projectionsService.AllProjectionsByCinema<ProjectionViewModel>(cinemaName).ToList(),
            };
            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public IActionResult ByProjectionId(int id)
        {
            var viewModel = this.projectionsService.ProjectionByProjectionId<ProjectionViewModel>(id);
            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public IActionResult ByMovieId(int id)
        {
            var viewModel = new AllProjectionsViewModel
            {
                AllProjections = this.projectionsService.ProjectionByMovieId<ProjectionViewModel>(id).ToList(),
            };

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public IActionResult FindProjection()
        {
            var movies = this.moviesService.AllMovies<MovieDropdownModel>();
            var cinemas = this.cinemaService.AllCinemas<CinemaDropdownModel>();

            var viewModel = new FindProjectionInputModel
            {
                Cinemas = cinemas,
                Movies = movies,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult FoundProjectionsResult(int cinemaId, int? movieId, DateTime? starttime)
        {
            var projectionsList = new List<ProjectionViewModel>();
            var viewModel = new AllProjectionsViewModel();
            int movieIdToFind = 0;
            DateTime toFind = DateTime.UtcNow;

            if (starttime == null && movieId != null)
            {
                movieIdToFind = movieId ?? default(int);
                projectionsList = this.projectionsService
                    .ProjectionByMovieIdAdCinemaIdOnly<ProjectionViewModel>((int)movieIdToFind, cinemaId)
                    .ToList();
                viewModel.AllProjections = projectionsList;
                return this.View(viewModel);
            }
            else
            {
                toFind = starttime ?? DateTime.Now;
            }

            if (movieId == null)
            {
                projectionsList = this.projectionsService
                    .ProjectionByCinemaIdAndDate<ProjectionViewModel>(cinemaId, toFind)
                    .ToList();
            }
            else
            {
                projectionsList = this.projectionsService
                    .ProjectionByMovieIdAdCinemaId<ProjectionViewModel>((int)movieId, cinemaId, toFind)
                    .ToList();
            }

            viewModel.AllProjections = projectionsList;

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
