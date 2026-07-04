namespace CSWMS.Models.ViewModel
{
    public class SalesReturnViewModel
    {
        public FeedabackModel FeedBackDetails { get; set; }
        public List<DepotModel> DepotList { get; set; }
        public List<SalesReturnApprovalIntial> SalesReturnApprovalList { get; set; }=new List<SalesReturnApprovalIntial>();
        public List<UserManagementModel> UserList { get; set; } = new List<UserManagementModel>();
    }
}
