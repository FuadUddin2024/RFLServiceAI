namespace CSWMS.Models
{
    public class ZoneModel
    {
        public int ZoneId { get; set; }
        public int? ZoneCode { get; set; }
        public string? ZoneName { get; set; }
        public string? ContactNo { get; set; }
        public int? ThanaId { get; set; }
        public int? DistrictId { get; set; }
        public string? EntryBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? EntryPC { get; set; }
        public string? ModifiedPC { get; set; }
    }
}
