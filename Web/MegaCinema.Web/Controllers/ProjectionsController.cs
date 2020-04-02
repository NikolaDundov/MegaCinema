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
        private readonly ApplicationDbContext context;

        public ProjectionsController(IProjectionsService projectionsService, ApplicationDbContext context)
        {
            this.projectionsService = projectionsService;
            this.context = context;
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

        public IActionResult FindProjection()
        {
            this.ViewData["CinemaId"] = new SelectList(this.context.Cinemas, "Id", "Id");
            this.ViewData["HallId"] = new SelectList(this.context.Halls, "Id", "Id");
            this.ViewData["MovieId"] = new SelectList(this.context.Movies, "Id", "Id");
            return this.View();
        }

        [HttpGet]
        public IActionResult FindProjection(string cinemaName)
        {
            var viewModel = new AllProjectionsViewModel
            {
                //AllProjections = this.projectionsService.AllProjections<ProjectionViewModel>(id).ToList(),
            };

            return this.View(viewModel);
        }
    }
}