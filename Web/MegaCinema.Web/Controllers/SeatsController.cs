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
        public IActionResult PickUpSeat(int id)
        {
            var projection = this.context.Projections.Find(id);
            var seats = this.context.Seats.Where(x => x.HallId == id);// && x.IsOccupied == false).Select(x=>x.SeatNumer);
            var seatsCollectionViewModel = new SeatsSelectModel();

            //var rows = seats.Where(x=>x.IsOccupied == false).Select(x => x.SeatNumer);
            //var avaialableSeats = seats.Where(x => x.IsOccupied == false).Select(x => x.SeatNumer);

            for (char row = 'A'; row <= 'L'; row++)
            {
                seatsCollectionViewModel.Rows.Add(row);

                foreach (var seat in seats.Where(x => x.Row == row && x.IsOccupied == false).Select(x => x.SeatNumer).OrderBy(x => x))
                {
                    seatsCollectionViewModel.Seats.Add(seat);
                }
            }

            return this.View(seatsCollectionViewModel);
        }

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
