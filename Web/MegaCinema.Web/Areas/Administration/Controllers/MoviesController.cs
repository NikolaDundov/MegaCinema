namespace MegaCinema.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MegaCinema.Common;
    using MegaCinema.Data;
    using MegaCinema.Data.Models;
    using MegaCinema.Services.Data;
    using MegaCinema.Web.ViewModels.Movie;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class MoviesController : Controller
    {
        private const string MovieTitleExists = "This movie title already exists!";
        private const int MoviesPerPageValue = 10;
        private readonly IMoviesService moviesService;
        private readonly IProjectionsService projectionsService;

        public MoviesController(
            IMoviesService moviesService,
            IProjectionsService projectionsService)
        {
            this.moviesService = moviesService;
            this.projectionsService = projectionsService;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Index(int page = 1, int perPage = MoviesPerPageValue)
        {
            var pagesCount = (int)Math.Ceiling(this.moviesService.MoviesCount() / (decimal)perPage);

            var viewModel = new AllIndexMovieViewModel
            {
                CurrentPage = page,
                PagesCount = pagesCount,
                AllMovies = this.moviesService.AllMovies<IndexMovieViewModel>()
                .Skip(perPage * (page - 1))
                .Take(perPage),
            };
            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var movie = await this.moviesService.GetByIdAsync<MovieViewModel>(id);
            if (movie == null)
            {
                return this.NotFound();
            }

            return this.View(movie);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(MovieInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            if (this.moviesService.MovieTitleExists(inputModel.Title))
            {
                this.ModelState.AddModelError("Title", MovieTitleExists);
                return this.View(inputModel);
            }

            var movieId = await this.moviesService.CreateMovie(inputModel);
            return this.RedirectToAction(nameof(this.Index));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var movie = await this.moviesService.GetByIdAsync<MovieInputModel>(id);
            if (movie == null)
            {
                return this.NotFound();
            }

            return this.View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieInputModel movie)
        {
            if (id != movie.Id)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(movie);
            }

            //if (this.moviesService.MovieTitleExists(movie.Title))
            //{
            //    this.ModelState.AddModelError("Title", MovieTitleExists);
            //    return this.View(movie);
            //}

            if (!this.moviesService.MovieExist(movie.Id))
            {
                return this.NotFound();
            }

            await this.moviesService.UpdateMovie(movie);
            return this.RedirectToAction(nameof(this.Index));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var movie = await this.moviesService.GetByIdAsync<MovieInputModel>(id);
            if (movie == null)
            {
                return this.NotFound();
            }

            return this.View(movie);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.projectionsService.DeleteByMovieId(id);
            await this.moviesService.DeleteById(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
