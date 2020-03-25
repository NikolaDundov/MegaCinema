namespace MegaCinema.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MegaCinema.Data;
    using MegaCinema.Data.Models;
    using MegaCinema.Services.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class TicketsController : BaseController
    {
        private readonly ITicketsService ticketsService;
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public TicketsController(ITicketsService ticketsService, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.ticketsService = ticketsService;
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult BookTicket()
        {
            //this.ViewData["ProjectionId"] = new SelectList(this.applicationDbContext.Projections, "Id", "Id");
            //this.ViewData["SeatId"] = new SelectList(this.applicationDbContext.Seats, "Id", "Id");
            //this.ViewData["UserId"] = new SelectList(this.applicationDbContext.Users, "Id", "Id");
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult BookTicket(int id, TicketType ticketType)
        {

            var loggedUser = this.userManager.GetUserId(this.User);
            var projection = this.context.Projections.Find(id);

            var ticket = new Ticket
            {
                ProjectionId = projection.Id,
                Movie = projection.Movie,
                Projection = projection,
                Type = ticketType,
                UserId = loggedUser,
                MovieId = projection.MovieId,
            };

            this.context.Tickets.Add(ticket);
            this.context.SaveChanges();
            //this.ViewData["ProjectionId"] = new SelectList(this.applicationDbContext.Projections, "Id", "Id");
            //this.ViewData["SeatId"] = new SelectList(this.applicationDbContext.Seats, "Id", "Id");
            //this.ViewData["UserId"] = new SelectList(this.applicationDbContext.Users, "Id", "Id");
            return this.RedirectToAction("/Seats/PickUpSeat");
        }
    }
}
