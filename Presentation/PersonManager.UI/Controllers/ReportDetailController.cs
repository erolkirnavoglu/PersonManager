using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PersonManager.UI.Helpers;
using PersonManager.UI.Models;

namespace PersonManager.UI.Controllers
{
    [Route("report-details")]
    public class ReportDetailController : Controller
    {
        private readonly ApiClient _apiClient;
        private readonly ApiRoot _api;
        public ReportDetailController(ApiClient apiClient, IOptions<ApiRoot> apiOptions)
        {
            _apiClient = apiClient;
            _api = apiOptions.Value;
        }

        [HttpGet("detail/{reportId}")]
        public async Task<IActionResult> GetByReportDetailList(Guid reportId)
        {
            var url = $"{_api.GetDetailList}/{reportId}";
            var details = await _apiClient.GetAsync<List<ReportDetailResponse>>(url);
            return Json(new { details });

        }
    }
}
