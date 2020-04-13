namespace MegaCinema.Web.ViewModels.Ticket
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;

    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;

    public class MyTicketsViewModel : IMapFrom<Ticket>
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [DisplayName("Movie Title")]
        public string MovieTitle { get; set; }

        public decimal Price { get; set; }

        public char Row { get; set; }

        [DisplayName("Seat Name")]
        public int SeatNumber { get; set; }

        public TicketType Type { get; set; }

        [DisplayName("Projection Time")]
        public DateTime ProjectionStartTime { get; set; }

        public string CinemaAdress { get; set; }

        [DisplayName("Booked on")]
        public DateTime CreatedOn { get; set; }

        public string PriceStr => string.Format("{0:C}", this.Price);
    }
}
