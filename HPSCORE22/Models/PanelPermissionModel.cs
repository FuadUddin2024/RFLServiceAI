namespace CSWMS.Models
{
    public class PanelPermissionModel
    {
    }
    public class panelPermissionSetupModel
    {
        public int UwPnlId { get; set; }
        public string? UserId { get; set; }
        public int? ZoneId { get; set; }
        public int? ItemId { get; set; }
        public bool? Active { get; set; }
        public bool FirstApv { get; set; }
        public bool FinalApv { get; set; }
        public string? EntryBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? EntryPC { get; set; }
        public string? ModifiedPC { get; set; }
    }
}
