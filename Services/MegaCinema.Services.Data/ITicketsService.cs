namespace MegaCinema.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using MegaCinema.Data.Models;
    using MegaCinema.Web.ViewModels.Ticket;

    public interface ITicketsService
    {
        TicketViewModel GetTicketDetails(int projectionId);

        Task<int> AddTicketAndSeat(int projectionId, string userId, char row, int seat, decimal price);

        BookedTicketViewModel ShowBookedTicket(int ticketId);

        IEnumerable<MyTicketsViewModel> ShowAllMyTickets();
    }
}
