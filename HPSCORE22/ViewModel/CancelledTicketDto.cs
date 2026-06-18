namespace CSWMS.ViewModel
{
    public class CancelledTicketDto
    {
        public string TicketCode { get; set; }

        public string CustomerName { get; set; }

        public string ContactNo { get; set; }

        public string ItemName { get; set; }
        public string ProblemName { get; set; }

        public string StatusName { get; set; }

        public DateTime EntryDate { get; set; }
    }
}
