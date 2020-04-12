namespace MegaCinema.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using MegaCinema.Services.Data;
    using MegaCinema.Web.ViewModels;
    using MegaCinema.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IMoviesService moviesService;

        public HomeController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexAllMoviesViewModel
            {
                AllMovies = this.moviesService.AllMovies<IndexMovieViewModel>()
                .Where(x => x.ReleaseDate < DateTime.UtcNow && x.Score >= 7.0)
                .ToList(),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
