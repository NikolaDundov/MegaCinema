﻿namespace MegaCinema.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MegaCinema.Services.Data;
    using MegaCinema.Web.ViewModels.Projection;
    using Microsoft.AspNetCore.Mvc;

    public class ProjectionsController : BaseController
    {
        private readonly IProjectionsService projectionsService;

        public ProjectionsController(IProjectionsService projectionsService)
        {
            this.projectionsService = projectionsService;
        }

        public IActionResult All()
        {
            var viewModel = new AllProjectionsViewModel
            {
                AllProjections = this.projectionsService.AllProjections<ProjectionViewModel>().ToList(),
            };

            return this.View(viewModel);
        }

        public IActionResult ByCinemaId(int id)
        {
            var viewModel = new AllProjectionsViewModel
            {
                AllProjections = this.projectionsService.AllProjectionsByCinema<ProjectionViewModel>(id).ToList(),
            };

            return this.View(viewModel);
        }

        public IActionResult ByProjectionId(int id)
        {
            var viewModel = this.projectionsService.ProjectionByProjectionId<ProjectionViewModel>(id);
            return this.View(viewModel);
        }

        public IActionResult ByMovieId(int id)
        {
            var viewModel = new AllProjectionsViewModel
            {
                AllProjections = this.projectionsService.ProjectionByMovieId<ProjectionViewModel>(id).ToList(),
            };

            return this.View(viewModel);
        }
    }
}