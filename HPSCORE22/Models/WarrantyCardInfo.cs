namespace CSWMS.Models
{
    public class WarrantyCardInfo
    {
        public int WacId { get; set; }
        public string? WaName { get; set; }
        public string? EntryBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? EntryPC { get; set; }
        public string? ModifiedPC { get; set; }
        public int ClassID { get; set; }
    }
}
