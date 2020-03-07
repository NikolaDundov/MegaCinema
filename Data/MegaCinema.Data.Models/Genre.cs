namespace MegaCinema.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MegaCinema.Data.Common.Models;

    public class Genre : BaseModel<int>
    {
        public GenreType Type { get; set; }
    }
}
