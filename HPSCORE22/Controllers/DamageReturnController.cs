using Microsoft.AspNetCore.Mvc;

namespace CSWMS.Controllers
{
    public class DamageReturnController : Controller
    {
        public IActionResult DamageReturnIntialApplication()
        {
            return View();
        }
    }
}
