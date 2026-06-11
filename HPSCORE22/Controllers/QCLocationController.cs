using Microsoft.AspNetCore.Mvc;
using QCMS.Models;
using QCMS.Services;

namespace QCMS.Controllers
{
    public class QCLocationController : Controller
    {
        private readonly QCLocationService _locationService;
        private readonly QCCompanyService _companyService;
        private readonly CommonService _commonService;

        // Constructor (FIXED)
        public QCLocationController(QCLocationService locationService,
                                    QCCompanyService companyService,
                                    CommonService commonService)
        {
            _locationService = locationService;
            _companyService = companyService;
            _commonService = commonService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _locationService.GetAllAsync();
            return View(data);
        }

        // Create / Edit Page
        public async Task<IActionResult> Create(string id)
        {
            //  Load dropdown
            ViewBag.CompanyList = await _companyService.GetAllAsync() ?? new List<QCCompany>();

            if (string.IsNullOrEmpty(id))
            {
                return View(new QC_Location());
            }

            //  FIXED SERVICE
            var data = await _locationService.GetByIdAsync(id);

            if (data == null)
            {
                return View(new QC_Location());
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(QC_Location model)
        {
            // Current User 
            //string currentUser = HttpContext.Session.GetString("USER") ?? "Admin";

            var currentUser = HttpContext.Session.GetString("USER_TEXT") ?? "Admin";

            if (string.IsNullOrEmpty(model.LOCATION_ID))
            {
                model.LOCATION_ID = await _commonService.GenerateIdAsync("QC_Location");
            }

            // Call Service
            var result = await _locationService.SaveAsync(model, currentUser);

            if (!result.success)
            {
                ViewBag.Error = result.message;

                // VERY IMPORTANT → Reload dropdown again
                ViewBag.CompanyList = await _companyService.GetAllAsync() ?? new List<QCCompany>();

                return View("Create", model);
            }

            //  Success
            TempData["Success"] = result.message;
            return RedirectToAction("Index");
        }
        // Delete
        public async Task<IActionResult> Delete(string id)
        {
            await _locationService.DeleteAsync(id);
            TempData["Success"] = "Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}