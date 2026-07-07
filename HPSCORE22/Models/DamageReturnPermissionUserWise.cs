namespace CSWMS.Models
{
    public class DamageReturnPermissionUserWise
    {
        public int UwDmId { get; set; }
        public string? UserId { get; set; }
        public int ItemId { get; set; }
        public int Active { get; set; }
        public bool C1stApv { get; set; }
        public bool C2ndApv { get; set; }
        public bool C3rdApv { get; set; }
        public bool FinalApv { get; set; }
        public string? EntryBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? EntryPC { get; set; }
        public string? ModifiedPC { get; set; }
    }

}
