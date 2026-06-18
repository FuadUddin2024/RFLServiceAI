using CSWMS.Models;
using CSWMS.Models.ViewModel;
using CSWMS.Repositories;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CSWMS.Controllers
{
    public class ComplainListController : Controller
    {
        private readonly ComplainListRepository _complainListRepository;
        private readonly ZoneRepository _ZoneDetails;
        private readonly CompanyRepository _CompanyDetails;
        private readonly AssignRepository _AssignList;
        private readonly SuperVisorRepository _SuperVisor;
        private readonly Repositories.StatusRepository _StatusDetails;
        private readonly Repositories.TechnicianRepository _technicians;
        public ComplainListController(ComplainListRepository complainListRepository,
            ZoneRepository Zones, CompanyRepository Company,AssignRepository AssingList,
            SuperVisorRepository SuperVisor, Repositories.StatusRepository StatusDetails,TechnicianRepository technicians)
        {
            _complainListRepository = complainListRepository;
            _ZoneDetails = Zones;
            _CompanyDetails = Company;
            _AssignList = AssingList;
            _SuperVisor = SuperVisor;
            _StatusDetails = StatusDetails;
            _technicians = technicians;
        }

        // Compalain -> Zone -> Super Visor (Assing)
        public IActionResult ComplainListData()
        {
            ComplainViewModel ComplainViewModel = new ComplainViewModel();
            ComplainViewModel.AssignModel=new AssignModel();
            ComplainViewModel.ZoneList = _ZoneDetails.GetALlZoneistForAssing();
          //  ComplainViewModel.SuperVisorList = _SuperVisor.GetAllSuperVisorList();
            ComplainViewModel.CompanyList = _CompanyDetails.GetALLCompany();
           ComplainViewModel.StatusList = _StatusDetails.GetALLStatus();
            return View(ComplainViewModel);
        }

        [HttpPost]
        public JsonResult GetAllComplainList()
        {
            var data = _complainListRepository.GetALlComplainListForAssing().OrderByDescending(x => x.EntryDate).ToList();

            return Json(new
            {
                complainList = data
            });
        }
        [HttpGet]
        public JsonResult GetComplainSingle(string id)
        {
            var data = _complainListRepository.GetAllComplainList(id);
            return Json(new
            {
                complainList = data
            });
        }
        [HttpGet]
        public JsonResult GetZoneWiseSupervisor(int zoneId)
        {
            var data = _SuperVisor.GetAllSuperVisorList().Where(x=>x.ZoneId== zoneId).ToList();
            return Json(new
            {
                SupervisorList = data
            });
        }
        [HttpPost]
        public IActionResult SaveAssignData(ComplainViewModel CompalinViewModel)
        {
            var AssignDataFrom = CompalinViewModel.AssignModel;
            if (ModelState.IsValid)
            {
                if (AssignDataFrom.TicketID > 0)
                {
                    var AssingData = _AssignList.GetAllComplainList(AssignDataFrom.TicketID.ToString()).FirstOrDefault();
                    if (AssingData != null)
                    {
                        TempData["ERRORMSG"] = "This Ticket ID is already exists for assign, please update this Ticket ID.";
                    }
                    else
                    {
                        AssignDataFrom.AssignDate = DateTime.Now;

                        AssignDataFrom.IsAssign = false;
                        AssignDataFrom.EntryDate = DateTime.Now;
                        AssignDataFrom.SendFeedback = false;
                        AssignDataFrom.EntryBy =  HttpContext.Session.GetString("USER_TEXT") ?? "Admin";
                        AssignDataFrom.SupervisorId= AssignDataFrom.SupervisorId;
                        var AssingID= _AssignList.InsertAssign(AssignDataFrom);
                        if(AssingID>0)
                        {
                            var data = _complainListRepository.GetAllComplainList(AssignDataFrom.TicketID.ToString()).FirstOrDefault();
                            data.SendAssign= true;
                            var UpdatedData= _complainListRepository.UpdateComplain(data);
                            if (UpdatedData)
                            {
                                TempData["SuccessMSG"] = "Successfully Inserted.";
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
                    }
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
            return RedirectToAction("ComplainListData", "ComplainList");
        }

        // Compalain -> Zone -> Super Visor-> Technician (Assing)

        public IActionResult TechnicianAssignList()
        {
            ComplainViewModel ComplainViewModel = new ComplainViewModel();
            ComplainViewModel.AssignModel = new AssignModel();
            ComplainViewModel.ZoneList = _ZoneDetails.GetALlZoneistForAssing();
            ComplainViewModel.SuperVisorList = _SuperVisor.GetAllSuperVisorList();
            ComplainViewModel.CompanyList = _CompanyDetails.GetALLCompany();
            ComplainViewModel.StatusList = _StatusDetails.GetALLStatus();
            return View(ComplainViewModel);
            //  return View();
        }
        [HttpPost]
        public JsonResult GetAllAssignZone()
        {
            var data = _AssignList.GetAllZoneAssingComplainList().OrderByDescending(x => x.EntryDate);

            return Json(new
            {
                complainList = data
            });
        }
        [HttpGet]
        public JsonResult GetAssignSingle(string id)
        {
            var data = _AssignList.GetAllZoneSingleAssing(id).FirstOrDefault();
            var TechnicianList= _technicians.GetAllTechnicianList().Where(x=>x.SupervisorId== data.SupervisorId).ToList();
            return Json(new
            {
                complainList = data,
                TechnicianList= TechnicianList
            });
        }
        [HttpPost]
        public IActionResult SaveAssignDataWithTechnician(ComplainViewModel CompalinViewModel)
        {
            var AssignDataFrom = CompalinViewModel.AssignModel;
            if (ModelState.IsValid)
            {
                if (AssignDataFrom.TicketID > 0)
                {
                    if(AssignDataFrom.TechnicianId>0)
                    {
                        var AssignData = _AssignList.GetAllZoneSingleAssing(AssignDataFrom.TicketID.ToString()).FirstOrDefault();
                        if (AssignData != null)
                        {
                            AssignData.IsAssign = true;
                            AssignData.SendFeedback = false;
                            AssignData.FinishDate = DateTime.Now;
                            var Updateddata = _AssignList.AssignPerson(AssignDataFrom);
                            if (Updateddata)
                            {
                                TempData["SuccessMSG"] = "Successfully Updated.";
                            }
                            else
                            {
                                TempData["ERRORMSG"] = "!!!! ERROR !!!";
                            }
                        }
                        else
                        {
                            TempData["ERRORMSG"] = "ERROR";
                        }
                    }
                    else
                    {
                        TempData["ERRORMSG"] = "Please Add Technician";
                    }
                }
                else
                {
                    TempData["ERRORMSG"] = "!!!! ERROR !!!";
                }
                
            }
            return RedirectToAction("TechnicianAssignList", "ComplainList");
        }

        // Zone and Technician Transfer
        public IActionResult TokenTransfer()
        {
            ComplainViewModel ComplainViewModel = new ComplainViewModel();
            ComplainViewModel.AssignModel = new AssignModel();
            ComplainViewModel.ZoneList = _ZoneDetails.GetALlZoneistForAssing();
            ComplainViewModel.SuperVisorList = _SuperVisor.GetAllSuperVisorList();
            ComplainViewModel.CompanyList = _CompanyDetails.GetALLCompany();
            ComplainViewModel.StatusList = _StatusDetails.GetALLStatus();
            return View(ComplainViewModel);
            //  return View();
        }
        [HttpPost]
        public JsonResult GetAllPendingTask()
        {
            var data = _AssignList.GetALLPendingToken().OrderByDescending(x => x.EntryDate);

            return Json(new
            {
                complainList = data
            });
        }
        [HttpGet]
        public JsonResult GetSingleAssingTokenPending(string id)
        {
            var data = _AssignList.GetAllZoneSingleAssingPending(id).FirstOrDefault();
            var TechnicianList = _technicians.GetAllTechnicianList().Where(x => x.SupervisorId == data.SupervisorId).ToList();
            return Json(new
            {
                complainList = data,
                TechnicianList = TechnicianList
            });
        }
        // Dependable Dropdown for Zone and Technician
        [HttpGet]
        public JsonResult GetZoneWiseTSuperVisor(string id)
        { 
            List<SuperVisorModel>superVisors= new List<SuperVisorModel>();
            if (id != null)
            {
                superVisors =  _SuperVisor.GetAllSuperVisorList().Where(x => x.ZoneId == Convert.ToInt32(id)).ToList();
            }
            return Json(new
            {
                SupervisorList = superVisors
            });
        }
        [HttpGet]
        public JsonResult GetSupervisorWiseTechnician(string id)
        {
            List<TechnicianModel> superVisors = new List<TechnicianModel>();
            if (id != null)
            {
                superVisors = _technicians.GetAllTechnicianList().Where(x => x.SupervisorId == Convert.ToInt32(id)).ToList();
            }
            return Json(new
            {
                TechnicianList = superVisors
            });
        }
        [HttpPost]
        public IActionResult SaveTransferTokenData(ComplainViewModel CompalinViewModel)
        {
            var AssignDataFrom = CompalinViewModel.AssignModel;
            if (ModelState.IsValid)
            {
                if (AssignDataFrom.TicketID > 0)
                {
                    var AssignData = _AssignList.GetAllZoneSingleAssingPending(AssignDataFrom.TicketID.ToString()).FirstOrDefault();
                    if (AssignData != null)
                    {
                        AssignData.CustomerAddress= AssignDataFrom.CustomerAddress;
                        AssignData.ProductName= AssignDataFrom.ProductName;
                        AssignData.AssignZoneId= AssignDataFrom.AssignZoneId;
                        AssignData.SupervisorId= AssignDataFrom.SupervisorId;
                        AssignData.TechnicianId= AssignDataFrom.TechnicianId;
                        AssignData.FinishDate = AssignDataFrom.FinishDate;
                        AssignDataFrom.ModifiedBy = HttpContext.Session.GetString("USER_TEXT") ?? "Admin";
                        AssignDataFrom.ModifiedDate = DateTime.Now;
                        var Updateddata = _AssignList.AssignPerson(AssignDataFrom);
                        if (Updateddata)
                        {
                            TempData["SuccessMSG"] = "Successfully Updated.";
                        }
                        else
                        {
                            TempData["ERRORMSG"] = "!!!! ERROR !!!";
                        }
                    }
                    else
                    {
                        TempData["ERRORMSG"] = "Please Add Technician";
                    }
                }
                else
                {
                    TempData["ERRORMSG"] = "!!!! ERROR !!!";
                }

            }
            return RedirectToAction("TechnicianAssignList", "ComplainList");
        }

    }
}