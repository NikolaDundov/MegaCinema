﻿// ReSharper disable VirtualMemberCallInConstructor
namespace MegaCinema.Data.Models
{
    using System;
    using System.Collections.Generic;

    using MegaCinema.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Tickets = new HashSet<Ticket>();
            this.DiscountCard = new MembershipCard { CardType = MembershipCardType.NoDiscount };
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public int MembershipCardId { get; set; }

        public virtual MembershipCard DiscountCard { get; set; }
    }
}
