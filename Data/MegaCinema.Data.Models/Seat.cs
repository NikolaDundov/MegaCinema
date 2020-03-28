namespace MegaCinema.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MegaCinema.Data.Common.Models;

    public class Seat : BaseModel<int>
    {
        public char Row { get; set; }

        public int SeatNumer { get; set; }

        public bool IsOccupied { get; set; }

        public int ProjectionId { get; set; }

        public virtual Projection Projection { get; set; }

        //public int TicketId { get; set; }

        //public virtual Ticket Ticket { get; set; }

        //public int HallId { get; set; }

        //public virtual Hall Hall { get; set; }

        public string Name => this.Row + this.SeatNumer.ToString();
    }
}
