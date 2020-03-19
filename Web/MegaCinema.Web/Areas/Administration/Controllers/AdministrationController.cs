namespace MegaCinema.Web.Areas.Administration.Controllers
{
    using MegaCinema.Common;
    using MegaCinema.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {

    }
}
