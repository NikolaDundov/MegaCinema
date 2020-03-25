namespace MegaCinema.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class SeatsController : BaseController
    {
        public SeatsController()
        {

        }

        public IActionResult PickUpSeat()
        {
            return this.View();
        }
    }
}
