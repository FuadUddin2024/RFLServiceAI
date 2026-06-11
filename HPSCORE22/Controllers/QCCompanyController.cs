using Microsoft.AspNetCore.Mvc;
using QCMS.Models;
using QCMS.Services;

namespace QCMS.Controllers
{
    public class QCCompanyController : Controller
    {
        private readonly QCCompanyService _service;
        private readonly CommonService _commonService;

        public QCCompanyController(QCCompanyService service, CommonService commonService)
        {
            _service = service;
            _commonService = commonService;
        }

        // List Page
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        // Create / Edit Page
        public async Task<IActionResult> Create(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View(new QCCompany());

            var data = await _service.GetByIdAsync(id);
            return View(data);
        }

        // Save (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(QCCompany model)
        {
            var currentUser = HttpContext.Session.GetString("USER_TEXT") ?? "Admin";

            if (string.IsNullOrEmpty(model.COMPANY_ID))
            {
                model.COMPANY_ID = await _commonService.GenerateIdAsync("QC_COMPANY");
            }

            var result = await _service.SaveAsync(model, currentUser);

            if (!result.success)
            {
                ViewBag.Error = result.message;
                return View("Create", model);
            }

            TempData["Success"] = result.message;
            return RedirectToAction("Index");
        }

        // Delete
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            TempData["Success"] = "Deleted successfully";
            return RedirectToAction("Index");
        }

        // List Page
        public async Task<IActionResult> Company()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }
        public async Task<IActionResult> AddCompany()
        {
            var data = await _service.GetAllAsync();
            return View();
        }
    }
}