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

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [Area("Administration")]
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

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [Area("Administration")]
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
            //ViewData["CinemaId"] = new SelectList(context.Cinemas, "Id", "Id");
            //ViewData["HallId"] = new SelectList(context.Halls, "Id", "Id");
            //ViewData["MovieId"] = new SelectList(context.Movies, "Id", "Id");
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [Area("Administration")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectionInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                //var projection = new Projection
                //{
                //    CinemaId = inputModel.ci
                //}

                //context.Add(projection);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CinemaId"] = new SelectList(context.Cinemas, "Id", "Id", projection.CinemaId);
            //ViewData["HallId"] = new SelectList(context.Halls, "Id", "Id", projection.HallId);
            //ViewData["MovieId"] = new SelectList(context.Movies, "Id", "Id", projection.MovieId);
            return this.View();

           // return this.View(projection);
        }

        // GET: Administration/Projections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
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
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(projection);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectionExists(projection.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["CinemaId"] = new SelectList(context.Cinemas, "Id", "Id", projection.CinemaId);
            ViewData["HallId"] = new SelectList(context.Halls, "Id", "Id", projection.HallId);
            ViewData["MovieId"] = new SelectList(context.Movies, "Id", "Id", projection.MovieId);
            return View(projection);
        }

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projection = await this.context.Projections.FindAsync(id);
            this.context.Projections.Remove(projection);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool ProjectionExists(int id)
        {
            return context.Projections.Any(e => e.Id == id);
        }
    }
}
