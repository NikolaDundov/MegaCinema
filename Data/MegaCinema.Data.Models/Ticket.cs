namespace MegaCinema.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using MegaCinema.Data.Common.Models;

    public class Ticket : BaseModel<int>
    {
        public int ProjectionId { get; set; }

        public virtual Projection Projection { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        public TicketType Type { get; set; }

        public char Row { get; set; }

        public int SeatNumer { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
