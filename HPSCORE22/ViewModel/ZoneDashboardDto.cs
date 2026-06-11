namespace CSWMS.ViewModel
{
    public class ZoneDashboardDto
    {
        public List<string> Labels { get; set; } = new();

        public List<decimal> TotalTickets { get; set; } = new();

        public List<decimal> Solved { get; set; } = new();

        public List<decimal> Pending { get; set; } = new();

        public List<decimal> Cancelled { get; set; } = new();
    }
}
