//using Microsoft.AspNetCore.Mvc;

//namespace CSWMS.Controllers
//{
//    public class DashboardController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}

using CSWMS.Repositories;
using Microsoft.AspNetCore.Mvc;

[Route("api/dashboard")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _service;

    public DashboardController(IDashboardService service)
    {
        _service = service;
    }

    [HttpGet("kpi")]
    public async Task<IActionResult> GetKpi()
    {
        var data = await _service.GetDashboardKpiAsync();

        return Ok(data);
    }

    //[HttpGet("daily-trend")]
    //public async Task<IActionResult> GetDailyTrend()
    //{
    //    return Ok(await _service.GetDailyTrendAsync());
    //}
    [HttpGet("daily-trend")]
    public async Task<IActionResult> GetDailyTrend()
    {
        var data = await _service.GetDailyTrendAsync();

        return Ok(data);
    }

    [HttpGet("status")]
    public async Task<IActionResult> GetStatus()
    {
        return Ok(new
        {
            values = await _service.GetStatusAsync()
        });
    }

    [HttpGet("zone")]
    public async Task<IActionResult> GetZone()
    {
        return Ok(await _service.GetZoneAsync());
    }

    [HttpGet("technician")]
    public async Task<IActionResult> GetTechnician()
    {
        return Ok(await _service.GetTechnicianAsync());
    }

    [HttpGet("bottom-technician")]
    public async Task<IActionResult> GetBottomTechnician()
    {
        return Ok(await _service.GetBottomTechnicianAsync());
    }

    [HttpGet("product-wise-service-analysis")]
    public async Task<IActionResult> ProductWiseServiceAnalysis()
    {
        //var data =
        //    await _dashboardRepository
        //        .GetProductWiseServiceAnalysisAsync();

        //return Ok(data);
        return Ok(await _service.GetProductWiseServiceAnalysisAsync());
    }
    [HttpGet("cancelled-details")]
    public async Task<IActionResult>
    GetCancelledDetails(string type)
    {
        var data =
            await _service.GetCancelledDetailsAsync(type);

        return Ok(data);
    }

    [HttpGet("warranty-kpi")]
    public async Task<IActionResult> WarrantyKpi()
    {
        var data =
            await _service
            .GetWarrantyDashboardAsync();

        return Ok(data);
    }

    [HttpGet("warranty-chart")]
    public async Task<IActionResult> WarrantyChart()
    {
        var data =
            await _service
            .GetWarrantyChartAsync();

        return Ok(data);
    }

    [HttpGet("service-operation-performance")]
    public async Task<IActionResult> GetServiceOperationPerformance()
    {
            return Ok(await _service.GetServiceOperationPerformance());
    }
}
