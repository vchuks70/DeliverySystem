using Domain.Helper.Request;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        public IReport ReportService { get; set; }

        public ReportController(IReport reportService)
        {
            ReportService = reportService;
        }

        [HttpPost]
        [Route("daily-orders")]
        public async Task<IActionResult> GetDailyOrders([FromBody] GetReportOrdersRequest model)
        {
            var result = await ReportService.GetDailyOrders(model);
            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpPost]
        [Route("most-popular-order")]
        public async Task<IActionResult> GetMostPopularOrder([FromBody] GetReportOrdersRequest model)
        {
            var result = await ReportService.GetMostPopularOrder(model);
            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpPost]
        [Route("most-popular/target-locations")]
        public async Task<IActionResult> GetMostPopularTargetLocationsResponse([FromBody] GetReportOrdersRequest model)
        {
            var result = await ReportService.GetMostPopularTargetLocationsResponse(model);
            return result.Any() ? Ok(result) : NoContent();
        }
    }
}
