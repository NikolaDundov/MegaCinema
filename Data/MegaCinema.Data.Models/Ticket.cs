namespace MegaCinema.Data.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public int ProjectioId { get; set; }

        public Projection Projection { get; set; }

        public decimal Price { get; set; }

        public TicketType Type { get; set; }
    }
}