namespace CSWMS.Models
{
    public class ComplainType
    {
        public int CTypeId { get; set; }
        public string? ComTypeName { get; set; }
        public string? EntryBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? EntryPC { get; set; }
        public string? ModifiedPC { get; set; }

    }
}
