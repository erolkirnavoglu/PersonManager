using Microsoft.AspNetCore.Mvc;
using PersonManager.UI.Helpers;
using PersonManager.UI.Models;

namespace PersonManager.UI.Controllers
{
    [Route("report-details")]
    public class ReportDetailController : Controller
    {
        private readonly ApiClient _apiClient;
        public ReportDetailController(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpGet("detail/{reportId}")]
        public async Task<IActionResult> GetByReportDetailList(Guid reportId)
        {
            var url = $"{ApiRoot.GetDetailList}/{reportId}";
            var details = await _apiClient.GetAsync<List<ReportDetailResponse>>(url);
            return Json(new { details });

        }
    }
}
