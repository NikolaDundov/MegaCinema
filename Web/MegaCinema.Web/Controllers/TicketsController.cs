namespace MegaCinema.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MegaCinema.Data;
    using MegaCinema.Data.Models;
    using MegaCinema.Services.Data;
    using MegaCinema.Web.ViewModels.Seats;
    using MegaCinema.Web.ViewModels.Ticket;
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

        [Authorize]
        public IActionResult BookTicket(int projectionId)
        {
            var viewModel = this.ticketsService.GetTicketDetails(projectionId);
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult BookTicket(TicketViewModel inputModel)
        {
            var loggedUser = this.userManager.GetUserId(this.User);

            return this.View(inputModel);
        }
    }
}
