using CSWMS.CommonMethod;
using CSWMS.Models;
using CSWMS.Models.ViewModel;
using CSWMS.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CSWMS.Controllers
{
    public class ApprovalSetupController : Controller
    {
        // Models For Approval setup process
        private ApproverSetupManagementViewModel ApproverSetup =new ApproverSetupManagementViewModel();
        // Models For Approval setup process
        // Repositories
        private readonly UserManagementRepository _UserManagement;
        private readonly ApproverManagementRepository _ApproverManagement;
        private readonly ItemRepository _itemRepo;
        private readonly SessionHelper _SessionHelper;
        public ApprovalSetupController(UserManagementRepository UserManagement, ApproverManagementRepository ApproverManagement, ItemRepository ItemRepo,
            SessionHelper SessionHelper)
        {
            _UserManagement = UserManagement;
            _ApproverManagement = ApproverManagement;
            _itemRepo = ItemRepo;
            _SessionHelper= SessionHelper;
        }
        // SR Setup start
        public IActionResult SalesRetrunSetup()
        {
            var userSession = _SessionHelper.GetUser();
            if (userSession != null)
            {
                if(userSession.CompanyID>0)
                {
                    ApproverSetup.UserList = _UserManagement.GetAllUserList();
                    ApproverSetup.SalesReturnItemList = _itemRepo.GetAllItemNames();
                }
            }
            return View("~/Views/SalesReturn/SalesRetrunSetup.cshtml", ApproverSetup);
        }
        public IActionResult SalesRetrunSetupSave(ApproverSetupManagementViewModel UserPermission,List<int> selectedcls)
        {
            var SessionUser= _SessionHelper.GetUser();
            if (ModelState.IsValid)
            {
                if(selectedcls.Count()>0)
                {
                    var existingPermission = _ApproverManagement.DeletePreviousPermissions(UserPermission.UserWiseSRPermission.UserId);
                    if (existingPermission)
                    {
                        foreach (var item in selectedcls)
                        {
                            UserPermission.UserWiseSRPermission.ItemId = item;
                            UserPermission.UserWiseSRPermission.EntryBy = SessionUser.USERID;
                            UserPermission.UserWiseSRPermission.EntryDate = DateTime.Now;
                            _ApproverManagement.InsertSRPermission(UserPermission.UserWiseSRPermission);
                        }
                        TempData["SuccessMSG"] = "Successfully Inserted.";
                    }
                    else
                    {
                        TempData["ERRORMSG"] = "Server Error. Please try again after some time";
                    }
                }
                else
                {
                    TempData["ERRORMSG"] = "Please Select Pages For permission";
                }
            }
            else
            {
                TempData["ERRORMSG"] = "Please select User Name or Approval Serial";
            }
            return RedirectToAction("SalesRetrunSetup", "ApprovalSetup");
        }
        public JsonResult GetUserWiseSRPermission(string UserId)
        {
            var data = _ApproverManagement.GetUserWiseSRPermission(UserId);
            if (data == null || !data.Any())
            {
                ViewData["Message"] = "No data found. Please select class.";
                return Json(new
                {
                    success = false,
                    message = "No data found. Please select class."
                });
            }
            return Json(new
            {
                success = true,
                data = data
            });
        }
        // SR Setup end
        //Panel Apply System Approver start

    
    }
}
