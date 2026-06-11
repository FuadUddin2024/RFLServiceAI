namespace CSWMS.Models
{
    public class StatusModel
    {
        public int StatusId { get; set; }
        public int? StatusCode { get; set; }
        public string? StatusName { get; set; }
        public string? EntryBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? EntryPC { get; set; }
        public string? ModifiedPC { get; set; }
    }
}
