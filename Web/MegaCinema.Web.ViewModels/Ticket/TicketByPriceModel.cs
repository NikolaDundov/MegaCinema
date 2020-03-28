namespace MegaCinema.Web.ViewModels.Ticket
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MegaCinema.Data.Models;

    public class TicketByPriceModel
    {
        public TicketType Type { get; set; }

        public decimal Price { get; set; }
    }
}
