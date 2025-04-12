using Microsoft.AspNetCore.Mvc;
using PersonManager.UI.Helpers;
using PersonManager.UI.Models;

namespace PersonManager.UI.Controllers
{
    [Route("person-infos")]
    public class PersonInfoController : Controller
    {
        private readonly ApiClient _apiClient;
        public PersonInfoController(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpGet("create")]
        public IActionResult Create([FromQuery]Guid personId)
        {
            ViewBag.PersonId = personId;
            return PartialView();
        }

        [HttpGet("personId/{personId}")]
        public async Task<IActionResult> GetPersonIdInfo(Guid personId)
        {
            var url = $"{ApiRoot.GetPersonIdInfo}/{personId}";
            var personInfos = await _apiClient.GetAsync<List<PersonInfoModel>>(url);
            return Json(new { personInfos });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PersonInfoPost model)
        {
            bool isSuccess = await _apiClient.PostAsync(ApiRoot.PostPersonInfo, model);
            if (!isSuccess)
            {
                ModelState.AddModelError(string.Empty, "Veri eklenirken bir hata oluştu.");
                return View(model);

            }
            return View(model);
        }

    }
}
