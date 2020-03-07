namespace MegaCinema.Data.Models
{
    using MegaCinema.Data.Common.Models;

    public class Seat : BaseModel<string>
    {
        public int HallId { get; set; }

        public virtual Hall Hall { get; set; }
    }
}
