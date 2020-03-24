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
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class TicketsController : BaseController
    {
        private readonly ITicketsService ticketsService;
        private readonly ApplicationDbContext applicationDbContext;

        public TicketsController(ITicketsService ticketsService, ApplicationDbContext applicationDbContext)
        {
            this.ticketsService = ticketsService;
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        [Authorize]
        public IActionResult BookTicket()
        {
            this.ViewData["ProjectionId"] = new SelectList(this.applicationDbContext.Projections, "Id", "Id");
            this.ViewData["SeatId"] = new SelectList(this.applicationDbContext.Seats, "Id", "Id");
            this.ViewData["UserId"] = new SelectList(this.applicationDbContext.Users, "Id", "Id");
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult BookTicket(int id)
        {
            //var ticket = new Ticket
            //{
            //    ProjectionId = id,
            //    MovieId
            //};
            this.ViewData["ProjectionId"] = new SelectList(this.applicationDbContext.Projections, "Id", "Id");
            this.ViewData["SeatId"] = new SelectList(this.applicationDbContext.Seats, "Id", "Id");
            this.ViewData["UserId"] = new SelectList(this.applicationDbContext.Users, "Id", "Id");
            return this.View();
        }
    }
}
