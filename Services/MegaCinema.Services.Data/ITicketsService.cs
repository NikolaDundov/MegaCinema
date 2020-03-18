namespace MegaCinema.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ITicketsService
    {
        void CreateTicket<T>(string userId, int projectionId, string projectionType, string ticketType);
    }
}
