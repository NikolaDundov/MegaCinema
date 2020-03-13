﻿namespace MegaCinema.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
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
                AllMovies = (ICollection<IndexMovieViewModel>)this.moviesService.AllMovies<IndexMovieViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult Test()
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
