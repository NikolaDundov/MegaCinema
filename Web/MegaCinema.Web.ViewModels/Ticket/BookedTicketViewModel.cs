namespace MegaCinema.Web.ViewModels.Ticket
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;

    using MegaCinema.Data.Models;

    public class BookedTicketViewModel
    {
        public string UserId { get; set; }

        public string MovieTitle { get; set; }

        public decimal Price { get; set; }

        public char Row { get; set; }

        public int SeatNumber { get; set; }

        public TicketType Type { get; set; }

        public DateTime ProjectionStartTime { get; set; }

        public string CinemaAdress { get; set; }
    }
}
