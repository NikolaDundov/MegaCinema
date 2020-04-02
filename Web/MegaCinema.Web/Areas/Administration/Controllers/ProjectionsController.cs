namespace MegaCinema.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using MegaCinema.Common;
    using MegaCinema.Web.ViewModels.Projection;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MegaCinema.Services.Data;
    using MegaCinema.Data.Models;
    using MegaCinema.Web.ViewModels.Cinema;

    [Area("Administration")]
    public class ProjectionsController : Controller
    {
        private const int PostsPerPageDefaultValue = 50;
        private readonly IProjectionsService projectionsService;
        private readonly IMoviesService moviesService;
        private readonly ICinemaService cinemaService;
        private readonly IHallService hallService;

        public ProjectionsController(
            IProjectionsService projectionsService,
            IMoviesService moviesService,
            ICinemaService cinemaService,
            IHallService hallService)
        {
            this.projectionsService = projectionsService;
            this.moviesService = moviesService;
            this.cinemaService = cinemaService;
            this.hallService = hallService;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Index(int page = 1, int perPage = PostsPerPageDefaultValue)
        {
            var pagesCount = (int)Math.Ceiling(this.projectionsService.ProjectionsCount() / (decimal)perPage);

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
            inputModel.Movies = this.moviesService.AllMovies<MovieDropdownModel>().OrderBy(x => x.Title);
            inputModel.Halls = this.hallService.GetAll<HallDropdownModel>().OrderBy(x => x.Name);
            inputModel.Cinemas = this.cinemaService.AllCinemas<CinemaDropdownModel>().OrderBy(x => x.City);

            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var cinema = this.cinemaService.GetCinemaById<CinemaHallsModel>(inputModel.CinemaId);
            if (!cinema.Halls.Any(x => x.Id == inputModel.HallId))
            {
                this.ModelState.AddModelError(string.Empty, "There isn't such hall in this cinema!");
                return this.View(inputModel);
            }

            var projectionId = await this.projectionsService.CreateAsync(
                inputModel.CinemaId,
                inputModel.StartTime,
                inputModel.MovieId,
                inputModel.HallId,
                inputModel.Type);

            return this.RedirectToAction(nameof(this.Details), new { id = projectionId });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var projection = this.projectionsService.ProjectionByProjectionId<ProjectionInputModel>(id);

            if (projection == null)
            {
                return this.NotFound();
            }

            projection.Movies = this.moviesService.AllMovies<MovieDropdownModel>();
            projection.Cinemas = this.cinemaService.AllCinemas<CinemaDropdownModel>();
            projection.Halls = this.hallService.GetAll<HallDropdownModel>();

            return this.View(projection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectionInputModel projection)
        {
            if (id != projection.Id)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                projection.Movies = this.moviesService.AllMovies<MovieDropdownModel>();
                projection.Cinemas = this.cinemaService.AllCinemas<CinemaDropdownModel>();
                projection.Halls = this.hallService.GetAll<HallDropdownModel>();

                return this.View(projection);
            }

            if (!this.projectionsService.ProjectionExists(projection.Id))
            {
                return this.NotFound();
            }

            await this.projectionsService.UpdateProjection(projection);
            return this.RedirectToAction(nameof(this.Details), new { id = projection.Id });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var projection = this.projectionsService.
                ProjectionByProjectionId<ProjectionDetailsAdminView>(id);

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
            await this.projectionsService.DeleteById(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
