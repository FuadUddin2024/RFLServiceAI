namespace CSWMS.ViewModel
{
    public class DailyTrendDto
    {
        public List<string> Labels { get; set; } = new();

        public List<int> TotalTickets { get; set; } = new();

        public List<int> SolvedTickets { get; set; } = new();
    }
}
