namespace MegaCinema.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MegaCinema.Services.Data;
    using MegaCinema.Web.ViewModels.Cinema;
    using Microsoft.AspNetCore.Mvc;

    public class CinemasController : BaseController
    {
        private readonly ICinemaService cinemaService;

        public CinemasController(ICinemaService cinemaService)
        {
            this.cinemaService = cinemaService;
        }

        public IActionResult Details(int id)
        {
            var viewModel = this.cinemaService.ShowProjections<CinemaViewModel>(id);
            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public IActionResult Sofia()
        {
            var city = "Sofia";
            var viewModel = this.cinemaService.ShowCinema<CinemaViewModel>(city);
            return this.View(viewModel);
        }

        public IActionResult All()
        {
            var viewModel = new CinemasAllViewModel
            {
                Cinemas = this.cinemaService.AllCinemas<CinemaViewModel>().ToList(),
            };
            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
