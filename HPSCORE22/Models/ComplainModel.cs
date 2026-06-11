namespace CSWMS.Models
{
    public class ComplainModel
    {
        public int TicketID { get; set; }
        public int? TicketCode { get; set; }
        public int? SalesInvoiceId { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerFatherName { get; set; }
        public string ContactNo { get; set; }
        public string AlternativeContactNo { get; set; }
        public string Location { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int? ProductQty { get; set; }
        public int? PrModelId { get; set; }
        public string ProSerialNo { get; set; }
        public int? problemId { get; set; }
        public string ProblemName { get; set; }
        public int? GroupId { get; set; }
        public int? StatusId { get; set; }
        public int? apid { get; set; }
        public string Assigned { get; set; }
        public bool? SendAssign { get; set; }
        public string EntryBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string EntryPC { get; set; }
        public string ModifiedPC { get; set; }
        public string fsp { get; set; }
        public int? technicianid { get; set; }
        public string technicianstaffid { get; set; }
    }
    public class ComplainAssignList
    {
        public int TicketID { get; set; }
        public int ? TicketCode { get; set; }
        public int ? SalesInvoiceId { get; set; }
        public  DateTime PurchaseDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerFatherName { get; set; }
        public string ContactNo { get; set; }
        public string AlternativeContactNo { get; set; }
        public string Location { get; set; }
        public int ? ProductId { get; set; }
        public string ProductName { get; set; }
        public int ? ProductQty { get; set; }
        public int ? PrModelId { get; set; }
        public string ProSerialNo { get; set; }
        public int ? problemId { get; set; }
        public string ProblemName { get; set; }
        public int ? GroupId { get; set; }
        public int ? StatusId { get; set; }
        public int ? apid { get; set; }
        public string Assigned { get; set; }
        public bool? SendAssign { get; set; }
        public string EntryBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime EntryDate { get; set; }
        public  DateTime ModifiedDate { get; set; }
        public string EntryPC { get; set; }
        public string ModifiedPC { get; set; }
    }
}
