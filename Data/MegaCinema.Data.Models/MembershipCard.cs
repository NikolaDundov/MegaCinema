namespace MegaCinema.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MegaCinema.Data.Common.Models;

    public class MembershipCard : BaseModel<int>
    {
        public int ApplicationUserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public MembershipCardType CardType { get; set; }
    }
}
