namespace MegaCinema.Web.ViewModels.Ticket
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;

    public class TicketViewModel : IMapFrom<Ticket>
    {
        public TicketViewModel()
        {
            this.Rows = new HashSet<char>();
            this.SeatNumbers = new HashSet<int>();
            this.TicketPrice = new HashSet<TicketByPriceModel>();
            this.OccupiedSeats = new HashSet<Seat>();
        }

        [Required]
        public int ProjectionId { get; set; }

        public int MovieId { get; set; }

        [Required]
        public decimal Price { get; set; }

        public TicketType Type { get; set; }

        [Required]
        public int SeatNumer { get; set; }

        [Required]
        public char Row { get; set; }

        public ICollection<char> Rows { get; set; }

        public ICollection<int> SeatNumbers { get; set; }

        public ICollection<TicketByPriceModel> TicketPrice { get; set; }

        public ICollection<Seat> OccupiedSeats { get; set; }
    }
}
