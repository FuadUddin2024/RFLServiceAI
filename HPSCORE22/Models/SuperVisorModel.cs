namespace CSWMS.Models
{
    public class SuperVisorModel
    {
        public int SupervisorId { get; set; }
        public int? SupervisorCode { get; set; }
        public string? SupervisorName { get; set; }
        public int? StaffID { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int? ZoneId { get; set; }
        public int? GroupId { get; set; }
        public int? CompanyId { get; set; }
        public int?  DepartmentId { get; set; }
        public int? DesignationId { get; set; }
        public string? EntryBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? EntryPC { get; set; }
        public string? ModifiedPC { get; set; }
    }
}
