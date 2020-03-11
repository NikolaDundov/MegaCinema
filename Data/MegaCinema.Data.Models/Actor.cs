namespace MegaCinema.Data.Models
{
    using System.Collections.Generic;

    using MegaCinema.Data.Common.Models;

    public class Actor : BaseModel<int>
    {
        public string Name { get; set; }

        public bool IsOscarNominee { get; set; }
    }
}
