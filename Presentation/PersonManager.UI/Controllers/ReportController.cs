using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PersonManager.UI.Helpers;
using PersonManager.UI.Models;

namespace PersonManager.UI.Controllers
{
    [Route("reports")]
    public class ReportController : Controller
    {
        private readonly ApiClient _apiClient;
        private readonly ApiRoot _api;
        public ReportController(ApiClient apiClient, IOptions<ApiRoot> apiOptions)
        {
            _apiClient = apiClient;
            _api = apiOptions.Value;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return View();
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            var reports = await _apiClient.GetAsync<List<ReportResponse>>(_api.GetReportList);
            return Json(new { data = reports });

        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ReportDto model)
        {
            bool isSuccess = await _apiClient.PostAsync(_api.PostReport, model);
            if (!isSuccess)
            {
                ModelState.AddModelError(string.Empty, "Veri eklenirken bir hata oluştu.");
                return View(model);

            }
            return Ok(isSuccess);
        }
    }
}
