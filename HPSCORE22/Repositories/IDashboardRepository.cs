//namespace CSWMS.Repositories
//{
//    public class IDashboardRepository
//    {
//    }
//}

using CSWMS.ViewModel;

public interface IDashboardRepository
{
    Task<DashboardKpiDto> GetDashboardKpiAsync();

    Task<DailyTrendDto> GetDailyTrendAsync();

    Task<ZoneDashboardDto> GetZoneAsync();

    Task<TechnicianPerformanceDto> GetTechnicianAsync();

    Task<List<int>> GetStatusAsync();
    Task<TechnicianPerformanceDto> GetBottomTechnicianAsync();
    Task<List<ProductWiseServiceAnalysisDto>> GetProductWiseServiceAnalysisAsync();
    Task<List<CancelledTicketDto>>GetCancelledDetailsAsync(string type);
    Task<WarrantyDashboardDto> GetWarrantyDashboardAsync();
    Task<WarrantyDashboardDto> GetWarrantyChartAsync();
}
