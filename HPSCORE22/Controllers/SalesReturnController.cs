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
        //public JsonResult GetFeedBackData(int TokenID)
        //{
        //    if(TokenID != null)
        //    {
        //        var FeedbackData = _FeedBackData.GetSingleFeedbackModel(TokenID.ToString()).FirstOrDefault();
        //        if(FeedbackData.SrDepoId == null)
        //        {
        //            var Srlist= _DepoNames.GetALLDepotName().Where(x => x.DepotId == FeedbackData.PsDepoId).FirstOrDefault();
        //            if (Srlist != null)
        //            {
        //                return Json(new
        //                {
        //                    FeedBackDetails = FeedbackData
        //                });
        //            }
        //            else
        //            {
        //                TempData["ERRORMSG"] = "This Ticket ID does not exists.";
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            TempData["ERRORMSG"] = "Already apply by this ticket ID.";
        //            return;
        //        }
        //    }
        //   else
        //    {
        //         TempData["ERRORMSG"] = "Please input the ticket ID.";
        //        return;
        //    }

        //}
    }
}