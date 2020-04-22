﻿namespace MegaCinema.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MegaCinema.Data.Models;
    using MegaCinema.Services.Data;
    using MegaCinema.Web.ViewModels.Ticket;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class TicketsController : BaseController
    {
        private const int PostsPerPageDefaultValue = 10;
        private const string OccupiedSeat = "This seat is already occupied";
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
            if (viewModel == null)
            {
                return this.NotFound();
            }

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

            var result = await this.ticketsService.AddTicketAndSeat(
                inputModel.ProjectionId,
                userId,
                inputModel.Row,
                inputModel.SeatNumer,
                inputModel.Price);

            if (result < 0)
            {
                this.ModelState.AddModelError(string.Empty, OccupiedSeat);
                return this.View(inputModel);
            }

            return this.RedirectToAction("BookedTicketDetails", new { ticketId = result });
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
        public IActionResult HistoryTicketDetails(int ticketId)
        {
            var viewModel = this.ticketsService.ShowBookedTicket(ticketId);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> MyTickets(int page = 1, int perPage = PostsPerPageDefaultValue)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;
            var ticketForUser = this.ticketsService.ShowAllMyTickets()
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.CreatedOn);

            var pagesCount = (int)Math.Ceiling(ticketForUser.Count() / (decimal)perPage);

            var viewModel = new AllMyTicketsViewModel
            {
                CurrentPage = page,
                PagesCount = pagesCount,
                AllTickets = ticketForUser.Skip(perPage * (page - 1)).Take(perPage),
            };

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
