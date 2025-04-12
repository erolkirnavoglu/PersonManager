using Microsoft.AspNetCore.Mvc;
using PersonManager.UI.Helpers;
using PersonManager.UI.Models;

namespace PersonManager.UI.Controllers
{
    [Route("reports")]
    public class ReportController : Controller
    {
        private readonly ApiClient _apiClient;
        public ReportController(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return View();
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            var reports = await _apiClient.GetAsync<List<ReportResponse>>(ApiRoot.GetReportList);
            return Json(new { data = reports });

        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ReportDto model)
        {
            bool isSuccess = await _apiClient.PostAsync(ApiRoot.PostReport, model);
            if (!isSuccess)
            {
                ModelState.AddModelError(string.Empty, "Veri eklenirken bir hata oluştu.");
                return View(model);

            }
            return Ok(isSuccess);
        }
    }
}
