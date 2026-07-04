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
        public JsonResult GetFeedBackData(int TokenID)
        {
            if(TokenID>0)
            {
                var feedbackData = _FeedBackData.GetSingleFeedbackModel(TokenID.ToString()).FirstOrDefault();
                if(feedbackData.SrDepoId ==0)
                {
                    var srlist = _FeedBackData.GetSingleSrinfoForApply(TokenID.ToString());
                    if(feedbackData != null)
                    {
                        return Json(new
                        {
                            success = true,
                            FeedBackDetails = feedbackData
                        });
                    }
                    else
                     {
                        return Json(new
                        {
                            success = false,
                            message = "This Ticket ID does not exist."
                        });
                    }
                }
                else
                                    {
                    return Json(new
                    {
                        success = false,
                        message = "Already applied by this Ticket ID."
                    });
                }
            }
            else
            {
                return Json(new
                {
                    success = false,
                    message = "Please input the Ticket ID."
                });
            }
        }

        // <summary> 1st Approval Code start</summary>
        public IActionResult SalesApprovalIntial()
        {
            SalesReturnViewModel SalesReturn = new SalesReturnViewModel();

            return View("~/Views/SalesReturn/SalesApprovalIntial.cshtml", SalesReturn);
        }
    }
}