namespace CSWMS.Models
{
    public class DepotDetailsModel
    {
        public int DepotId { get; set; }
        public string DepotName { get; set; }
        public string DICName { get; set; }
        public string DICEmail { get; set; }
        public string SICName { get; set; }
        public string SICEmail { get; set; }
        public string EntryBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string EntryPC { get; set; }
        public string ModifiedPC { get; set; }
    }
}
