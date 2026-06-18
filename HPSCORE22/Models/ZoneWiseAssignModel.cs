namespace CSWMS.Models
{
    public class ZoneWiseAssignModel
    {
        public int? AssignId { get; set; }
        public int? TicketID { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerContactNo { get; set; }
        public string? CustomerAddress { get; set; }
        public string? ProductName { get; set; }
        public string? ProblemName { get; set; }
        public bool? IsAssign { get; set; }
        public bool? SendFeedback { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
        public int? AssignZoneId { get; set; }
        public string? ZoneName { get; set; }
        public int? SupervisorId { get; set; }
        public string? SupervisorName { get; set; }
        public int? TechnicianId { get; set; }
        public string? TechnicianName { get; set; }
        public int? StaffID { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? AssignDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string? Remarks { get; set; }
        public string? fsp { get; set; }
    }
}
