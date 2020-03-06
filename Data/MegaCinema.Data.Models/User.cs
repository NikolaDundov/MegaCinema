namespace MegaCinema.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MegaCinema.Data.Common.Models;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser, IDeletableEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        public DiscountCardType DiscountCard { get; set; }
    }
}
