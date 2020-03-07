namespace MegaCinema.Data.Models
{
    using System.Collections.Generic;

    using MegaCinema.Data.Common.Models;

    public class Actor : BaseModel<int>
    {
        public Actor()
        {
            this.Movies = new HashSet<MovieActor>();
        }

        public string Name { get; set; }

        public bool IsOscarNomiee { get; set; }

        public virtual ICollection<MovieActor> Movies { get; set; }
    }
}
