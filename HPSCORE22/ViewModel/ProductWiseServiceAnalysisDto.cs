namespace CSWMS.ViewModel
{
    public class ProductWiseServiceAnalysisDto
    {
        public string ItemName { get; set; }

        public int TotalTickets { get; set; }

        public int SolvedTickets { get; set; }

        public int PendingTickets { get; set; }

        public int ClosedOrCancelledTickets { get; set; }

        public double AvgResolutionDays { get; set; }
        public double SolvedPercentage { get; set; }
    }
}
