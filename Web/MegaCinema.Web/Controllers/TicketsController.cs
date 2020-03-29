namespace MegaCinema.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
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
        private readonly UserManager<ApplicationUser> userManager;

        public TicketsController(
            ITicketsService ticketsService,
            UserManager<ApplicationUser> userManager)
        {
            this.ticketsService = ticketsService;
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
        public async Task<IActionResult> BookTicket(TicketViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;

            var result = await this.ticketsService.AddTicketAndSeat(inputModel.ProjectionId, userId, inputModel.Row, inputModel.SeatNumer, inputModel.Price);

            if (result < 0)
            {
                this.ModelState.AddModelError(string.Empty, "The seat is already occupied");
                return this.View(inputModel);
            }

            return this.Redirect($"/Tickets/BookedTicketDetails?ticketId={result}");
        }

        [Authorize]
        public IActionResult BookedTicketDetails(int ticketId)
        {
            var viewModel = this.ticketsService.ShowBookedTicket(ticketId);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> MyTickets()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;
            var viewModel = this.ticketsService.ShowAllMyTickets().Where(x => x.UserId == userId);

            return this.View(viewModel);
        }
    }
}
