namespace MegaCinema.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class CinemasController : BaseController
    {
        public IActionResult Cinema()
        {

            return this.View();
        }
    }
}
