namespace MegaCinema.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MegaCinema.Common;
    using MegaCinema.Data;
    using MegaCinema.Data.Models;
    using MegaCinema.Web.ViewModels.Projection;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class ProjectionsController : Controller
    {
        private readonly ApplicationDbContext context;

        public ProjectionsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [Area("Administration")]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Index()
        {
            var projections = await this.context.Projections.ToListAsync();
            foreach (var projection in projections)
            {
                projection.Cinema = await this.context.Cinemas.Where(x => x.Id == projection.CinemaId).FirstOrDefaultAsync();
                projection.Movie = await this.context.Movies.Where(x => x.Id == projection.MovieId).FirstOrDefaultAsync();
                projection.Hall = await this.context.Halls.Where(x => x.Id == projection.HallId).FirstOrDefaultAsync();
            }

            return this.View(projections);
            //var applicationDbContext = this.context.Projections.Include(p => p.Cinema).Include(p => p.Hall).Include(p => p.Movie);
            //return View(await applicationDbContext.ToListAsync());
        }

        [Area("Administration")]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var projection = await this.context.Projections.FirstOrDefaultAsync(x => x.Id == id);
            projection.Cinema = await this.context.Cinemas.Where(x => x.Id == projection.CinemaId).FirstOrDefaultAsync();
            projection.Movie = await this.context.Movies.Where(x => x.Id == projection.MovieId).FirstOrDefaultAsync();
            projection.Hall = await this.context.Halls.Where(x => x.Id == projection.HallId).FirstOrDefaultAsync();

            //var projection = await context.Projections
            //    .Include(p => p.Cinema)
            //    .Include(p => p.Hall)
            //    .Include(p => p.Movie)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (projection == null)
            {
                return this.NotFound();
            }

            return this.View(projection);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [Area("Administration")]
        public IActionResult Create()
        {
            //var projections = new TestProjectionView
            //{
            //    CinemaNames = this.context.Cinemas.Select(x => x.City).ToList(),
            //    MovieNames = this.context.Movies.Select(t => t.Title).ToList(),
            //    HallsNames = this.context.Halls.Select(n => n.Name).ToList(),
            //};

            this.ViewData["CinemaId"] = new SelectList(this.context.Cinemas, "Id", "Id");
            this.ViewData["HallId"] = new SelectList(this.context.Halls, "Id", "Id");
            this.ViewData["MovieId"] = new SelectList(this.context.Movies, "Id", "Id");

            return this.View();
        }

        [HttpPost]
        [Area("Administration")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create([Bind("CinemaId,StartTime,MovieId,HallId,Type,Id,CreatedOn,ModifiedOn")] Projection projection)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(projection);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CinemaId"] = new SelectList(this.context.Cinemas, "Id", "Id", projection.CinemaId);
            this.ViewData["HallId"] = new SelectList(this.context.Halls, "Id", "Id", projection.HallId);
            this.ViewData["MovieId"] = new SelectList(this.context.Movies, "Id", "Id", projection.MovieId);
            projection.Seats = CreateRectangleSeatsHall('A', 16);
            return this.View(projection);

            //var cinema = this.context.Cinemas.FirstOrDefault(x => x.City == inputModel.CinemaCity);
            //var hall = this.context.Halls.FirstOrDefault(x => x.Name == inputModel.HallName);
            //var movie = this.context.Movies.FirstOrDefault(x => x.Title == inputModel.MovieTitle);

            //var projection = new Projection
            //{
            //    Cinema = cinema,
            //    CinemaId = cinema.Id,
            //    Hall = hall,
            //    HallId = hall.Id,
            //    Movie = movie,
            //    MovieId = movie.Id,
            //    StartTime = inputModel.StartTime,
            //    Type = inputModel.Type,
            //};

            //return this.View(projection);
        }

        [Area("Administration")]
        [ValidateAntiForgeryToken]
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

        [Area("Administration")]
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

        private static List<Seat> CreateRectangleSeatsHall(char lastRow, int firstRowSeatsCount)
        {
            var seats = new List<Seat>();
            for (char row = 'A'; row <= lastRow; row++)
            {
                for (int seatNumber = 1; seatNumber <= firstRowSeatsCount; seatNumber++)
                {
                    var seat = new Seat { Row = row, SeatNumer = seatNumber };
                    seats.Add(seat);
                }
            }

            return seats;
        }
    }
}
