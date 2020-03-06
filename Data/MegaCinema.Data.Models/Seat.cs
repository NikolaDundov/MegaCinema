using MegaCinema.Data.Common.Models;

namespace MegaCinema.Data.Models
{
    public class Seat : BaseDeletableModel<string>
    {
        public int HallId { get; set; }

        public Hall Hall { get; set; }
    }
}