namespace CSWMS.Models
{
    public class SalesReturnModel
    {
    }
    public class UserWiseSRPermission
    {
        public int UwSrPid { get; set; }
        public string? UserId { get; set; }
        public int ItemId { get; set; }
        public bool? Active { get; set; }
        public bool InitialApv { get; set; }
        public bool FirstApv { get; set; }
        public bool SecApv { get; set; }
        public bool FinalApv { get; set; }
        public string? EntryBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? EntryPC { get; set; }
        public string? ModifiedPC { get; set; }
    }
    public class ProductClassModelSalesReturn
    {
        public int ItemId { get; set; }
        public int? ItemCode { get; set; }
        public string? ClassName { get; set; }
        public string? Description { get; set; }
        public string? InstallationType { get; set; }
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public int? GroupId { get; set; }
        public string? BrandName { get; set; }
    }
}
