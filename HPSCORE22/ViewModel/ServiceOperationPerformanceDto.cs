namespace CSWMS.ViewModel
{
    public class ServiceOperationPerformanceDto
    {
        public string ServiceOperationName { get; set; }

        public int TotalTickets { get; set; }

        public int Solved { get; set; }

        public int Pending { get; set; }

        public int Cancelled { get; set; }

        public decimal SolveRate { get; set; }

        public decimal PendingRate { get; set; }

        public decimal CancelledRate { get; set; }

        public int ActiveTechnicians { get; set; }

        public decimal AvgTicketsPerTechnician { get; set; }
        public int PendingOver7Days { get; set; }

        public int PendingOver30Days { get; set; }
    }
}
