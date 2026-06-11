namespace CSWMS.ViewModel
{
    public class WarrantyDashboardDto
    {

            public int TotalTickets { get; set; }
            public int InWarranty { get; set; }
            public int OutWarranty { get; set; }
            public int WarrantyFound { get; set; }
            public int WarrantyNotFound { get; set; }
            public decimal WarrantyPercentage { get; set; }
        
    }
}
