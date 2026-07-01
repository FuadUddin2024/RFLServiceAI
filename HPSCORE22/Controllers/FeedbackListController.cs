using CSWMS.Models;
using CSWMS.Models.ViewModel;
using CSWMS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QCMS.Models;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QCMS.Controllers
{
    public class FeedbackListController : Controller
    {
        private readonly FeedBackRepository _FeedbackRepo;
        private readonly AssignRepository _AssignData;
        private readonly BrandRepository _BrandData;
        private readonly StatusRepository _StatusDetails;
        private readonly ItemRepository _itemdata;
        private readonly ProductRepository _ProductModel;
        private readonly CommonItemRepository _CommonItem;
        private readonly TechnicianRepository _Technicians;
        public FeedbackListController(FeedBackRepository feedback, AssignRepository assignData,BrandRepository BrandDetails, StatusRepository statusDetails,
            ItemRepository itemdetails, ProductRepository productmodel, CommonItemRepository CommonItem, TechnicianRepository Technicians)
        {
            _FeedbackRepo = feedback;
            _AssignData = assignData;
            _BrandData = BrandDetails;
            _StatusDetails = statusDetails;
            _itemdata= itemdetails;
            _ProductModel= productmodel;
            _CommonItem= CommonItem;
            _Technicians = Technicians;
        }
        public IActionResult Index()
        {
            FeedBackViewModel FeedbackDetails= new FeedBackViewModel();
            FeedbackDetails.FeedBackList = _FeedbackRepo.GetZoneWiseFeedBack(1).OrderByDescending(x=>x.EntryDate).ToList();
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
                    FeedbackDetails.FeedBackDetails.ProductQty =1;
                    FeedbackDetails.BrandList= _BrandData.GetALLBrand();
                    FeedbackDetails.StatusList = _StatusDetails.GetALLStatus();
                    FeedbackDetails.WarrantyCardList = _CommonItem.GetALLWarrantyCardInfo();
                    FeedbackDetails.SolvedBy= _Technicians.GetAllTechnicianList();
                    FeedbackDetails.AssistBy1= _Technicians.GetAllTechnicianList();
                    FeedbackDetails.AssistBy2= _Technicians.GetAllTechnicianList();
                    FeedbackDetails.ComplainTypes= _CommonItem.GetALLComplainType();
                }
            }
            return View("~/Views/FeedbackList/FeedbackPage.cshtml", FeedbackDetails);
        }

        // Ajax code feedbacks
        [HttpPost]
        public JsonResult GetAllClassProducts(int BrandID)
        {
            var Itemdata = _itemdata.GetAllClassNameBrandWise(BrandID);
            return Json(new
            {
                Filtereddata = Itemdata
            });
        }
        [HttpPost]
        public JsonResult GetAllProducts(int ItemID)
        {
            var Itemdata = _ProductModel.GetAllProductClassWise(ItemID);
            return Json(new
            {
                Filtereddata = Itemdata,
            });
        }
        [HttpPost]
        public JsonResult GetAllActualProblem(int productID)
        {
            var ActualProblemData = _FeedbackRepo.GetProductyWiseActualProblem(productID);
            return Json(new
            {
                NatureofProblem = ActualProblemData
            });
        }
        [HttpPost]
        public JsonResult GetAllSubProblems(int ProblemID)
        {
            var ActualProblemSubData = _FeedbackRepo.GetProductyWiseSubProblem(ProblemID);
            return Json(new
            {
                NatureofSubProblem = ActualProblemSubData
            });
        }
        public IActionResult FeedbackSave(FeedabackModel Feedback)
        {
            if(Feedback != null)
            {
                if(ModelState.IsValid)
                {
                    Feedback.EntryBy= HttpContext.Session.GetString("UserName");
                    Feedback.EntryDate= DateTime.Now;
                }
                else
                {
                    TempData["ERRORMSG"] = "!!!! ERROR !!!";
                }
            }
            else
            {
                TempData["ERRORMSG"] = "!!!! ERROR !!!";
            }
            return View("~/Views/FeedbackList/Index.cshtml");
        }
    }
}
