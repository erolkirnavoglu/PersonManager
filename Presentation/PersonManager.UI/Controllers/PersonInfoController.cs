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
            if (model is null)
            {
                ModelState.AddModelError(string.Empty, "Form Empty.");
                return Ok(false);
            }

            if (!ModelState.IsValid)
            {
                return Ok(false);
            }

            bool isSuccess = await _apiClient.PostAsync(ApiRoot.PostPersonInfo, model);
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
            var url = $"{ApiRoot.DeletePersonInfo}/{id}";
            bool isSuccess = await _apiClient.DeleteAsync(url);
            return Ok(isSuccess);
        }

    }
}
