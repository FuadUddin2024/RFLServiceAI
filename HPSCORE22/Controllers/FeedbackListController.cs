using QCMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace QCMS.Controllers
{
    public class FeedbackListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
