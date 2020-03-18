namespace MegaCinema.Web.ViewModels.Ticket
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MegaCinema.Data.Models;
    using MegaCinema.Services.Mapping;

    public class TicketViewModel : IMapFrom<Ticket>
    {
        public int Id { get; set; }

        public int ProjectionId { get; set; }

        public virtual Projection Projection { get; set; }

        public decimal Price { get; set; }

        public TicketType Type { get; set; }

        public int SeatId { get; set; }

        public virtual Seat Seat { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
