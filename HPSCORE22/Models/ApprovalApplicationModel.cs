namespace CSWMS.Models
{
    public class ApprovalApplicationModel
    {
    }
    public class SalesReturnApplication
    {
        public int FeedbackId { get; set; }
        public int TicketID { get; set; }
        public DateTime EntryDate { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerContactNo { get; set; }
        public string? CustomerAddress { get; set; }
        public int StatusId { get; set; }
        public string? StatusName { get; set; }
        public int ProductQty { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? SerialNo { get; set; }
        public int ActualProblemId { get; set; }
        public string? ProblemName { get; set; }
        public int PrModelId { get; set; }
        public string? PrModelName { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public int AssignZoneId { get; set; }
        public string? ZoneName { get; set; }
        public int DillerCode { get; set; }
        public string? SRInfo { get; set; }
        public string? SRMode { get; set; }
        public string? SRrejectCause { get; set; }
        public int SrDepoId { get; set; }
        public string? SrApplyBy { get; set; }
        public DateTime SrApplyDate { get; set; }
        public string? SrAppCheck { get; set; }
        public DateTime SrAppCheckDate { get; set; }
        public string? Sr1stApv { get; set; }
        public DateTime Sr1stApvDate { get; set; }
        public string? Sr2ndApv { get; set; }
        public DateTime Sr2ndApvDate { get; set; }
        public string? SrFinalApv { get; set; }
        public DateTime SrFinalApvDate { get; set; }
        public string? SrNoInBy { get; set; }
        public string? SrImage1 { get; set; }
        public string? SrImage2 { get; set; }
    }
    public class SalesReturnApprovalIntial
    {
        public int FeedbackId { get; set; }
        public int TicketID { get; set; }
        public DateTime EntryDate { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerContactNo { get; set; }
        public string? CustomerAddress { get; set; }
        public int StatusId { get; set; }
        public string? StatusName { get; set; }
        public int ProductQty { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? SerialNo { get; set; }
        public int ActualProblemId { get; set; }
        public string? ProblemName { get; set; }
        public int PrModelId { get; set; }
        public string? PrModelName { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public int AssignZoneId { get; set; }
        public string? ZoneName { get; set; }
        public int DillerCode { get; set; }
        public string? SRInfo { get; set; }
        public string? SRMode { get; set; }
        public string? SRrejectCause { get; set; }
        public int SrDepoId { get; set; }
        public string? DepotName { get; set; }
        public string? SrApplyBy { get; set; }
        public DateTime SrApplyDate { get; set; }
        public string? SrAppCheck { get; set; }
        public DateTime SrAppCheckDate { get; set; }
        public string? Sr1stApv { get; set; }
        public DateTime Sr1stApvDate { get; set; }
        public string? Sr2ndApv { get; set; }
        public DateTime Sr2ndApvDate { get; set; }
        public string? SrFinalApv { get; set; }
        public DateTime SrFinalApvDate { get; set; }
        public string? SrNoInBy { get; set; }
        public string? SrImage1 { get; set; }
        public string? SrImage2 { get; set; }
        public bool InitialApv { get; set; }
        public bool FirstApv { get; set; }
        public bool SecApv { get; set; }
        public bool FinalApv { get; set; }
        public bool Active { get; set; }
        public string? UserId { get; set; }
    }
}
