namespace CSWMS.Models
{
    public class UserManagementModel
    {
        public int UserCode { get; set; }
        public string? BU_Code { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public int? StaffID { get; set; }
        public int UserTypeId { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public string? Address { get; set; }
        public int? CompanyId { get; set; }
        public int? DepartmentId { get; set; }
        public int? DesignationId { get; set; }
        public int? ZoneId { get; set; }
        public int? DepotId { get; set; }
        public bool? DepoAct { get; set; }
        public bool? IsNew { get; set; }
        public bool? IsActive { get; set; }
        public string? ServiceUser { get; set; }
        public string? EntryBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? EntryPC { get; set; }
        public string? ModifiedPC { get; set; }
        public bool? MailPermission { get; set; }
        public bool? OfferPer { get; set; }
        public bool? VigoPer { get; set; }
        public bool? MenuPer { get; set; }

        //public virtual BusinessUnit BusinessUnit { get; set; }
    }
}
