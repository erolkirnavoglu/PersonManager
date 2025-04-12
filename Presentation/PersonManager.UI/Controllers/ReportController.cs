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
            var persons = await _apiClient.GetAsync<List<PersonResponse>>(ApiRoot.GetReportList);
            return Json(new { data = persons });

        }
    }
}
