namespace MegaCinema.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MegaCinema.Services.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class TicketsController : BaseController
    {
        private readonly ITicketsService ticketsService;

        public TicketsController(ITicketsService ticketsService)
        {
            this.ticketsService = ticketsService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult BookTicket()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult BookTicket(string userId, int projectionId, string projectionType, string ticketType)
        {
            return this.View();
        }
    }
}
