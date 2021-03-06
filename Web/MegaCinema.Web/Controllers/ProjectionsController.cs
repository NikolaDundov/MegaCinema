﻿namespace MegaCinema.Web.Controllers
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
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public class ProjectionsController : BaseController
    {
        private const string ErrorBeforeToday = "Date connot be before current date";
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
                AllProjections = this.projectionsService
                .AllProjectionsByCinema<ProjectionViewModel>(cinemaName)
                .ToList(),
            };

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public IActionResult ByProjectionId(int id)
        {
            var viewModel = this.projectionsService
                .ProjectionByProjectionId<ProjectionViewModel>(id);

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
                AllProjections = this.projectionsService
                .ProjectionByMovieId<ProjectionViewModel>(id)
                .ToList(),
            };

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public IActionResult FindProjection(int? movieId)
        {
            var viewModel = new FindProjectionInputModel();
            IEnumerable<MovieDropdownModel> movies;
            IEnumerable<CinemaDropdownModel> cinemas;

            if (movieId == null)
            {
                movies = this.moviesService.AllMovies<MovieDropdownModel>()
                        .Where(x => x.ReleaseDate.DayOfYear < DateTime.UtcNow.DayOfYear);
                cinemas = this.cinemaService.AllCinemas<CinemaDropdownModel>();
            }
            else
            {
                var movie = this.moviesService.MovieExist(movieId.Value);
                if (movie == false)
                {
                    movies = this.moviesService.AllMovies<MovieDropdownModel>()
                        .Where(x => x.ReleaseDate.DayOfYear < DateTime.UtcNow.DayOfYear);
                    cinemas = this.cinemaService.AllCinemas<CinemaDropdownModel>();
                }
                else
                {
                    movies = this.moviesService.AllMovies<MovieDropdownModel>()
                            .Where(x => x.Id == movieId);
                    cinemas = this.cinemaService.AllCinemas<CinemaDropdownModel>();
                }
            }

            viewModel.Cinemas = cinemas;
            viewModel.Movies = movies;
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult FoundProjectionsResult(int cinemaId, int? movieId, DateTime? starttime)
        {
            var projectionsList = new List<ProjectionViewModel>();
            var viewModel = new AllProjectionsViewModel();
            int movieIdToFind = 0;
            DateTime toFind = DateTime.UtcNow;

            if (starttime.HasValue)
            {
                DateTime haveDate = starttime.Value;
                if (haveDate.DayOfYear < toFind.DayOfYear)
                {
                    this.ModelState.AddModelError(string.Empty, ErrorBeforeToday);
                    var movies = this.moviesService.AllMovies<MovieDropdownModel>();
                    var cinemas = this.cinemaService.AllCinemas<CinemaDropdownModel>();

                    var inputViewModel = new FindProjectionInputModel
                    {
                        Cinemas = cinemas,
                        Movies = movies,
                    };

                    return this.RedirectToAction("FindProjection", inputViewModel);
                }
            }

            if (starttime == null && movieId != null)
            {
                movieIdToFind = movieId ?? default;
                projectionsList = this.projectionsService
                    .ProjectionByMovieIdAndCinemaIdOnly<ProjectionViewModel>(movieIdToFind, cinemaId)
                    .ToList();
                viewModel.AllProjections = projectionsList;
                return this.View(viewModel);
            }
            else
            {
                toFind = starttime ?? DateTime.UtcNow;
            }

            if (movieId == null)
            {
                if (toFind.DayOfYear == DateTime.UtcNow.DayOfYear)
                {
                    projectionsList = this.projectionsService
                    .ProjectionByCinemaIdAndDate<ProjectionViewModel>(cinemaId, toFind)
                    .Where(x => x.StartTime.Hour > DateTime.UtcNow.AddHours(3).Hour)
                    .ToList();
                }
                else
                {
                    projectionsList = this.projectionsService
                        .ProjectionByCinemaIdAndDate<ProjectionViewModel>(cinemaId, toFind)
                        .ToList();
                }
            }
            else
            {
                if (toFind.DayOfYear == DateTime.UtcNow.DayOfYear)
                {
                    projectionsList = this.projectionsService
                    .ProjectionByMovieIdAdCinemaId<ProjectionViewModel>((int)movieId, cinemaId, toFind)
                    .Where(x => x.StartTime.Hour > DateTime.UtcNow.AddHours(3).Hour)
                    .ToList();
                }
                else
                {
                    projectionsList = this.projectionsService
                        .ProjectionByMovieIdAdCinemaId<ProjectionViewModel>((int)movieId, cinemaId, toFind)
                        .ToList();
                }
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
