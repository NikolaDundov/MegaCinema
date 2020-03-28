namespace MegaCinema.Web.ViewModels.Ticket
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;

    public class TicketViewModel
    {
        public TicketViewModel()
        {
            this.Rows = new HashSet<char>();
            this.SeatNumbers = new HashSet<int>();
            this.TicketPrice = new HashSet<TicketByPriceModel>();
        }

        public int ProjectionId { get; set; }

        public int MovieId { get; set; }

        public decimal Price { get; set; }

        public TicketType Type { get; set; }

        public int SeatNumer { get; set; }

        public char Row { get; set; }

        public ICollection<char> Rows { get; set; }

        public ICollection<int> SeatNumbers { get; set; }

        public ICollection<TicketByPriceModel> TicketPrice { get; set; }
    }
}
