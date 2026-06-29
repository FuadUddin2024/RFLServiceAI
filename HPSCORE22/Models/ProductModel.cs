namespace CSWMS.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public int ProductCode { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public int ItemId { get; set; }
        public string? SerialNo { get; set; }
        public string? PartsNo { get; set; }
        public int ProductQty { get; set; }
        public decimal Cost { get; set; }
        public decimal UnitPrice { get; set; }
        public string? InstallationType { get; set; }
        public int CompanyId { get; set; }
        public bool Active { get; set; }
        public string? EntryBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? EntryPC { get; set; }
        public string? ModifiedPC { get; set; }
    }
}
