namespace CSWMS.ViewModel
{
    public class MonthCompareDto
    {
        public int TotalTicketsThisMonth { get; set; }
        public int TotalTicketsLastMonth { get; set; }

        public int TotalAssignThisMonth { get; set; }
        public int TotalAssignLastMonth { get; set; }

        public int SolvedThisMonth { get; set; }
        public int SolvedLastMonth { get; set; }

        public int PendingThisMonth { get; set; }
        public int PendingLastMonth { get; set; }

        public int CancelledThisMonth { get; set; }
        public int CancelledLastMonth { get; set; }

        public double AvgResolutionThisMonth { get; set; }
        public double AvgResolutionLastMonth { get; set; }
    }
}