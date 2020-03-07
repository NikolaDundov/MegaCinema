namespace MegaCinema.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MegaCinema.Data.Common.Models;

    public class Country : BaseModel<int>
    {
        public string Name { get; set; }
    }
}
