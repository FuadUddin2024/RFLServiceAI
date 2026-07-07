using Microsoft.AspNetCore.Mvc;

namespace CSWMS.Controllers
{
    public class PanelPermissionController : Controller
    {
        public IActionResult PanelIntialApply()
        {
            return View();
        }
    }
}
