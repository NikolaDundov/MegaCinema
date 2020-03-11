namespace MegaCinema.Data.Models
{
    using MegaCinema.Data.Common.Models;

    public class Ticket : BaseModel<int>
    {
        public int ProjectionId { get; set; }

        public virtual Projection Projection { get; set; }

        public decimal Price { get; set; }

        public TicketType Type { get; set; }

        public int SeatId { get; set; }

        public virtual Seat Seat { get; set; }
    }
}
