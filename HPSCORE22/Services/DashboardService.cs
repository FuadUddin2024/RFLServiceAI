//namespace CSWMS.Services
//{
//    public class DashboardService
//    {
//    }
//}
namespace CSWMS.Repositories;
using CSWMS.ViewModel;

public class DashboardService : IDashboardService
{
    private readonly IDashboardRepository _repository;

    public DashboardService(IDashboardRepository repository)
    {
        _repository = repository;
    }

    public async Task<DashboardKpiDto> GetDashboardKpiAsync()
    {
        return await _repository.GetDashboardKpiAsync();
    }

    public async Task<DailyTrendDto> GetDailyTrendAsync()
    {
        return await _repository.GetDailyTrendAsync();
    }

    public async Task<ZoneDashboardDto> GetZoneAsync()
    {
        return await _repository.GetZoneAsync();
    }

    public async Task<TechnicianPerformanceDto> GetTechnicianAsync()
    {
        return await _repository.GetTechnicianAsync();
    }

    public async Task<List<int>> GetStatusAsync()
    {
        return await _repository.GetStatusAsync();
    }
    public async Task<TechnicianPerformanceDto> GetBottomTechnicianAsync()
    {
        return await _repository.GetBottomTechnicianAsync();
    }
    public async Task<List<ProductWiseServiceAnalysisDto>> GetProductWiseServiceAnalysisAsync()
    {
        return await _repository.GetProductWiseServiceAnalysisAsync();
    }

    public async Task<List<CancelledTicketDto>> GetCancelledDetailsAsync(string type    )
    {
        return await _repository.GetCancelledDetailsAsync(type);
    }
    public async Task<WarrantyDashboardDto> GetWarrantyDashboardAsync()
    {
        return await _repository.GetWarrantyDashboardAsync();
    }
    public async Task<WarrantyDashboardDto> GetWarrantyChartAsync()
    {
        return await _repository.GetWarrantyChartAsync();
    }

}
