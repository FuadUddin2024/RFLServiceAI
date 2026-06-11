namespace CSWMS.ViewModel
{
    public class TechnicianPerformanceDto
    {
        public List<string> Labels { get; set; } = new();

        public List<int> TotalTickets { get; set; } = new();

        public List<int> Solved { get; set; } = new();

        public List<int> Pending { get; set; } = new();

        public List<int> Cancelled { get; set; } = new();

        public List<decimal> SolveRate { get; set; } = new();
    }
}
