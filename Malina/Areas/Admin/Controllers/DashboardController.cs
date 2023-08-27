using Microsoft.AspNetCore.Mvc;

namespace Malina.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
