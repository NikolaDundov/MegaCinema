namespace MegaCinema.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MegaCinema.Common;
    using MegaCinema.Data;
    using MegaCinema.Services.Mapping;
    using MegaCinema.Data.Models;
    using MegaCinema.Web.ViewModels.Projection;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using MegaCinema.Services.Data;

    [Area("Administration")]
    public class ProjectionsController : Controller
    {
        private const int PostsPerPageDefaultValue = 50;
        private readonly ApplicationDbContext context;
        private readonly IProjectionsService projectionsService;
        private readonly IMoviesService moviesService;
        private readonly ICinemaService cinemaService;
        private readonly IHallService hallService;

        public ProjectionsController(ApplicationDbContext context,
            IProjectionsService projectionsService,
            IMoviesService moviesService,
            ICinemaService cinemaService,
            IHallService hallService)
        {
            this.context = context;
            this.projectionsService = projectionsService;
            this.moviesService = moviesService;
            this.cinemaService = cinemaService;
            this.hallService = hallService;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Index(int page = 1, int perPage = PostsPerPageDefaultValue)
        {
            var pagesCount = (int)Math.Ceiling(this.context.Projections.Count() / (decimal)perPage);

            var viewModel = new AllProjectionsAdminModel
            {
                CurrentPage = page,
                PagesCount = pagesCount,
                Projections = this.projectionsService.AllProjectionsAdminArea().Skip(perPage * (page - 1))
                .Take(perPage),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var projection = this.projectionsService.ProjectionByProjectionId<ProjectionDetailsAdminView>(id);

            if (projection == null)
            {
                return this.NotFound();
            }

            return this.View(projection);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            var movies = this.moviesService.AllMovies<MovieDropdownModel>();
            var cinemas = this.cinemaService.AllCinemas<CinemaDropdownModel>();
            var halls = this.hallService.GetAll<HallDropdownModel>();

            var viewModel = new ProjectionInputModel
            {
                Cinemas = cinemas,
                Halls = halls,
                Movies = movies,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(ProjectionInputModel inputModel)
        {
            var projection = AutoMapperConfig.MapperInstance.Map<Projection>(inputModel);
            ////var hall = this.hallService.
            //if (projection.CinemaId != projection.Hall.CinemaId)
            //{
            //    return this.View(inputModel);
            //}

            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var projectioId = await this.projectionsService.CreateAsync(inputModel.CinemaId, inputModel.StartTime
                ,inputModel.MovieId, inputModel.HallId, inputModel.Type);

            return this.RedirectToAction(nameof(this.Details), projectioId);

        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var projection = await this.context.Projections.FindAsync(id);
            if (projection == null)
            {
                return this.NotFound();
            }

            //ViewData["CinemaId"] = new SelectList(context.Cinemas, "Id", "Id", projection.CinemaId);
            //ViewData["HallId"] = new SelectList(context.Halls, "Id", "Id", projection.HallId);
            //ViewData["MovieId"] = new SelectList(context.Movies, "Id", "Id", projection.MovieId);

            projection.Cinema = await this.context.Cinemas.Where(x => x.Id == projection.CinemaId).FirstOrDefaultAsync();
            projection.Movie = await this.context.Movies.Where(x => x.Id == projection.MovieId).FirstOrDefaultAsync();
            projection.Hall = await this.context.Halls.Where(x => x.Id == projection.HallId).FirstOrDefaultAsync();
            return this.View(projection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CinemaId,StartTime,MovieId,HallId,Type,Id,CreatedOn,ModifiedOn")] Projection projection)
        {
            if (id != projection.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(projection);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.ProjectionExists(projection.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CinemaId"] = new SelectList(this.context.Cinemas, "Id", "Id", projection.CinemaId);
            this.ViewData["HallId"] = new SelectList(this.context.Halls, "Id", "Id", projection.HallId);
            this.ViewData["MovieId"] = new SelectList(this.context.Movies, "Id", "Id", projection.MovieId);
            return this.View(projection);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var projection = await this.context.Projections.FindAsync(id);
            projection.Cinema = await this.context.Cinemas.Where(x => x.Id == projection.CinemaId).FirstOrDefaultAsync();
            projection.Movie = await this.context.Movies.Where(x => x.Id == projection.MovieId).FirstOrDefaultAsync();
            projection.Hall = await this.context.Halls.Where(x => x.Id == projection.HallId).FirstOrDefaultAsync();

            if (projection == null)
            {
                return this.NotFound();
            }

            return this.View(projection);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projection = await this.context.Projections.FindAsync(id);
            this.context.Projections.Remove(projection);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool ProjectionExists(int id)
        {
            return this.context.Projections.Any(e => e.Id == id);
        }
    }
}
