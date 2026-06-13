using System.ComponentModel.DataAnnotations;
namespace CSWMS.Models
{
    public class AssignModel
    {
        [Key]
        public int AssignId { get; set; }
        public int? AssignCode { get; set; }
        [Required]
        public int? TicketID { get; set; }
        public DateTime? AssignDate { get; set; }
        public DateTime? FinishDate { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public int? StatusId { get; set; }
        public int? TechnicianId { get; set; }
        [Required(ErrorMessage = "Please Select Zone")]
        public int? AssignZoneId { get; set; }
        public int? ZoneCode { get; set; }
        [Required(ErrorMessage = "Please Select Super Visor")]
        public int? SupervisorId { get; set; }
        [Required(ErrorMessage = "Please Select Company")]
        public int? CompanyId { get; set; }
        public int? GroupId { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerContactNo { get; set; }
        public string? CustomerAddress { get; set; }
        public string? ProductName { get; set; }
        public string? ProblemName { get; set; }
        public string? OtherReceiver { get; set; }
        public string? Remarks { get; set; }
        public bool? IsAssign { get; set; }
        public bool? SendFeedback { get; set; }
        public string? EntryBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? EntryPC { get; set; }
        public string? ModifiedPC { get; set; }
        public int? BillAmount { get; set; }
        public string? BillNote { get; set; }
        public DateTime? BillDate { get; set; }
        public string? BillFlag { get; set; }
        public string? UserId { get; set; }
        public int? NetAmount { get; set; }
        public string? Discount { get; set; }
        public decimal? Amount { get; set; }
        public string? fsp { get; set; }
        public string? staffid { get; set; }

        // Navigation properties
        public string ? ZoneName { get; set; }
        public string ? SupervisorName { get; set; }
        public string ? CompanyName { get; set; }
        public string ? StatusName { get; set; }
    }
}
