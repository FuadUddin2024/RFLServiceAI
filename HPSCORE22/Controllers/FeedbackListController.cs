using QCMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CSWMS.Repositories;
using CSWMS.Models.ViewModel;

namespace QCMS.Controllers
{
    public class FeedbackListController : Controller
    {
        private readonly FeedBackRepository _FeedbackRepo;
        private readonly AssignRepository _AssignData;
        public FeedbackListController(FeedBackRepository feedback, AssignRepository assignData)
        {
            _FeedbackRepo = feedback;
            _AssignData = assignData;
        }
        public IActionResult Index()
        {
            FeedBackViewModel FeedbackDetails= new FeedBackViewModel();
            FeedbackDetails.FeedBackList = _FeedbackRepo.GetZoneWiseFeedBack(1);
            return View("~/Views/FeedbackList/Index.cshtml", FeedbackDetails);
        }
        public IActionResult FeedBackPage(string id)
        {
            FeedBackViewModel FeedbackDetails = new FeedBackViewModel();
            if (id != null)
            {
                var TokenDetails = _AssignData.GetAllZoneSingleAssingDetails(id);
                FeedbackDetails.FeedBackDetails=new CSWMS.Models.FeedabackModel();
                if (TokenDetails != null)
                {
                    FeedbackDetails.FeedBackDetails.TicketID = (int)TokenDetails.TicketID;
                    FeedbackDetails.FeedBackDetails.CustomerName= TokenDetails.CustomerName;
                    FeedbackDetails.FeedBackDetails.CustomerContactNo= TokenDetails.CustomerContactNo;
                    FeedbackDetails.FeedBackDetails.CustomerAddress= TokenDetails.CustomerAddress;
                    FeedbackDetails.FeedBackDetails.ProductName= TokenDetails.ProductName;
                }
            }
            return View("~/Views/FeedbackList/FeedbackPage.cshtml", FeedbackDetails);
        }
    }
}
