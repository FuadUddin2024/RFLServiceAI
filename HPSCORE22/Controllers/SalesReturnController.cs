using CSWMS.Models;
using CSWMS.Models.ViewModel;
using CSWMS.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CSWMS.Controllers
{
    public class SalesReturnController : Controller
    {
        private readonly DepotRepository _DepoNames;
        private readonly FeedBackRepository _FeedBackData;

        public SalesReturnController(DepotRepository DepotRepo,FeedBackRepository FeedBackData)
        {
            _DepoNames = DepotRepo;
            _FeedBackData = FeedBackData;
        }
        public IActionResult SalesReturnIndex()
        {
            SalesReturnViewModel SalesReturn = new SalesReturnViewModel();
            SalesReturn.FeedBackDetails = new FeedabackModel();
            SalesReturn.DepotList = _DepoNames.GetALLDepotName();
            return View("~/Views/SalesReturn/SalesReturnIndex.cshtml", SalesReturn);
        }
        public JsonResult GetFeedBackData(string TokenID)
        {
            var FeedbackData = _FeedBackData.GetSingleFeedbackModel(TokenID);
            return Json(new
            {
                FeedbackData = FeedbackData
            });
        }
    }
}