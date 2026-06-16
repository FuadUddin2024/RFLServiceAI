namespace CSWMS.Models
{
    public class TechnicianModel
    {
        public int RacTid { get; set; }
        public string? TechName { get; set; }
        public int? StaffID { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int? ZoneId { get; set; }
        public string? EntryBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? EntryPC { get; set; }
        public string? ModifiedPC { get; set; }
    }
}
