namespace MegaCinema.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MegaCinema.Data;
    using MegaCinema.Data.Models;
    using MegaCinema.Web.ViewModels.Seats;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class SeatsController : BaseController
    {
        private readonly ApplicationDbContext context;

        public SeatsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [Authorize]


        [HttpPost]
        [Authorize]
        public IActionResult PickUpSeat(int seat, char row)
        {

            var choosenSeat = new Seat
            {
                Row = row,
                SeatNumer = seat,

            };
            return this.RedirectToAction();
        }
    }
}
