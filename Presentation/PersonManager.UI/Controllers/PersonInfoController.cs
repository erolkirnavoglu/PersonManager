using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PersonManager.UI.Helpers;
using PersonManager.UI.Models;

namespace PersonManager.UI.Controllers
{
    [Route("person-infos")]
    public class PersonInfoController : Controller
    {
        private readonly ApiClient _apiClient;
        private readonly ApiRoot _api;
        public PersonInfoController(ApiClient apiClient, IOptions<ApiRoot> apiOptions)
        {
            _apiClient = apiClient;
            _api = apiOptions.Value;
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
            var url = $"{_api.GetPersonIdInfo}/{personId}";
            var personInfos = await _apiClient.GetAsync<List<PersonInfoModel>>(url);
            return Json(new { personInfos });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PersonInfoPost model)
        {
            if (model is null)
            {
                ModelState.AddModelError(string.Empty, "Form Empty.");
                return Ok(false);
            }

            if (!ModelState.IsValid)
            {
                return Ok(false);
            }

            bool isSuccess = await _apiClient.PostAsync(_api.PostPersonInfo, model);
            if (!isSuccess)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while adding data.");
                return View(model);

            }
            return Ok(isSuccess);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var url = $"{_api.DeletePersonInfo}/{id}";
            bool isSuccess = await _apiClient.DeleteAsync(url);
            return Ok(isSuccess);
        }

    }
}
