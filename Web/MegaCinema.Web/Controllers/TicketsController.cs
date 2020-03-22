﻿namespace MegaCinema.Web.Controllers
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
        public IActionResult BookTicket(int id)
        {
            return this.View();
        }
    }
}
