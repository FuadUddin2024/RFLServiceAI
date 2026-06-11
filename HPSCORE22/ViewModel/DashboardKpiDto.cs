namespace CSWMS.ViewModel
{
    public class DashboardKpiDto
    {
        public int TotalTickets { get; set; }
        public int TotalAssign { get; set; }

        public int Solved { get; set; }
        public int Pending { get; set; }
        public int Cancelled { get; set; }

        public int ThisMonthTickets { get; set; }
        public int LastMonthTickets { get; set; }

        public int ThisMonthSolved { get; set; }
        public int LastMonthSolved { get; set; }

        public int ThisMonthCancelled { get; set; }
        public int LastMonthCancelled { get; set; }

        public decimal TicketGrowthPercent { get; set; }
        public decimal SolvedGrowthPercent { get; set; }
        public decimal CancelGrowthPercent { get; set; }

        public decimal TicketGrowth { get; set; }
        public decimal SolvedGrowth { get; set; }
        public decimal CancelGrowth { get; set; }

        public int ThisMonthPending { get; set; }
        public int LastMonthPending { get; set; }
        public decimal PendingGrowth { get; set; }
        public int TotalTechnician { get; set; }
        public int TotalSupervisor { get; set; }
        public int TotalServiceCenter { get; set; }
        public decimal ThisMonthSolvedPercentage { get; set; }
        public decimal LastMonthSolvedPercentage { get; set; }
        public decimal TotalSolvedPercentage { get; set; }
    }
}
