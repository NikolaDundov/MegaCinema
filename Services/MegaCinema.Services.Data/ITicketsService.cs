namespace MegaCinema.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ITicketsService
    {
        T CreateTicket<T>(int id);
    }
}
