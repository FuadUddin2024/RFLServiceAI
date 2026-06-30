namespace CSWMS.Models
{
    public class BrandModel
    {
        public int GroupId { get; set; }
        public int GroupCode { get; set; }
        public string? GroupName { get; set; }
        public int ProductId { get; set; }
        public string? EntryBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? EntryPC { get; set; }
        public string? ModifiedPC { get; set; }
    }
}
