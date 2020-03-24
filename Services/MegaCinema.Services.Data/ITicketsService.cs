namespace MegaCinema.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MegaCinema.Web.ViewModels.Ticket;

    public interface ITicketsService
    {
        TicketViewModel CreateTicket(int id);
    }
}
