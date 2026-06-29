namespace CSWMS.Models
{
    public class ItemModel
    {
        public int ItemId { get; set; }
        public int ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? Description { get; set; }
        public string? InstallationType { get; set; }
        public int CompanyId { get; set; }
        public int GroupId { get; set; }
        public string? EntryBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? EntryPC { get; set; }
        public string? ModifiedPC { get; set; }
        public int WarrantyStatus { get; set; }
    }
}
