namespace MegaCinema.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MegaCinema.Data.Common.Models;

    public class Projection : BaseModel<int>
    {
        public Projection()
        {
            this.Seats = new HashSet<Seat>();
            this.Tickets = new HashSet<Ticket>();
        }

        public int CinemaId { get; set; }

        public virtual Cinema Cinema { get; set; }

        public DateTime StartTime { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        public int HallId { get; set; }

        public virtual Hall Hall { get; set; }

        public ProjectionType Type { get; set; }

        public ICollection<Seat> Seats { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        private static List<Seat> CreateRectangleSeatsHall(char lastRow, int firstRowSeatsCount)
        {
            var seats = new List<Seat>();
            for (char row = 'A'; row <= lastRow; row++)
            {
                for (int seatNumber = 1; seatNumber <= firstRowSeatsCount; seatNumber++)
                {
                    var seat = new Seat { Row = row, SeatNumer = seatNumber };
                    seats.Add(seat);
                }
            }

            return seats;
        }
    }

}
