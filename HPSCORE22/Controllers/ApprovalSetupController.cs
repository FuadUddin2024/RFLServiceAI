using CSWMS.Models.ViewModel;
using CSWMS.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CSWMS.Controllers
{
    public class ApprovalSetupController : Controller
    {
        private AppointmentManagementViewModel AppointmentSetup =new AppointmentManagementViewModel();
        private readonly UserManagementRepository _UserManagement;
        private readonly ApproverManagementRepository _ApproverManagement;
        private readonly ItemRepository _itemRepo;
        public ApprovalSetupController(UserManagementRepository UserManagement, ApproverManagementRepository ApproverManagement, ItemRepository ItemRepo)
        {
            _UserManagement = UserManagement;
            _ApproverManagement = ApproverManagement;
            _itemRepo = ItemRepo;
        }
        public IActionResult SalesRetrunSetup()
        {
            AppointmentSetup.UserList= _UserManagement.GetAllUserList();
            AppointmentSetup.SalesReturnItemList= _itemRepo.GetAllItemNames();
            return View("~/Views/SalesReturn/SalesRetrunSetup.cshtml", AppointmentSetup);
        }
    }
}
