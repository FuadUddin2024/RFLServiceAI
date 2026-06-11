namespace CSWMS.Models
{
    public class CompanyModel
    {
        public int CompanyId { get; set; }
        public int? CompanyCode { get; set; }
        public string? CompanyName { get; set; }
        public string? EntryBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? EntryPC { get; set; }
        public string? ModifiedPC { get; set; }
    }
}
