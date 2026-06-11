namespace CSWMS.Models.ViewModel
{
    public class ComplainViewModel
    {
        public AssignModel AssignModel {  get; set; }
        public List<ComplainAssignList> CompalinListforAssing { get; set; } = new List<ComplainAssignList>();
        public List<ZoneModel> ZoneList { get; set; } = new List<ZoneModel>();
        public List<SuperVisorModel> SuperVisorList { get; set; }= new List<SuperVisorModel>();
        public List<CompanyModel> CompanyList { get; set; } = new List<CompanyModel>();
        public List<StatusModel> StatusList { get; set; } = new List<StatusModel>();
    }
}
