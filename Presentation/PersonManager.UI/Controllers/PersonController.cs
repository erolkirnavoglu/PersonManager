using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PersonManager.UI.Helpers;
using PersonManager.UI.Models;
using System.Reflection;

namespace PersonManager.UI.Controllers
{
    [Route("persons")]
    public class PersonController : Controller
    {
        private readonly ApiClient _apiClient;
        private readonly ApiRoot _api;
        public PersonController(ApiClient apiClient, IOptions<ApiRoot> apiOptions)
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
            var persons = await _apiClient.GetAsync<List<PersonResponse>>(_api.GetPersonList);
            return Json(new { data = persons });

        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PersonPost model)
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
            bool isSuccess = await _apiClient.PostAsync(_api.PostPerson, model);
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
            var url = $"{_api.DeletePerson}/{id}";
            bool isSuccess = await _apiClient.DeleteAsync(url);
            return Ok(isSuccess);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfo(Guid id)
        {
            var url = $"{_api.ByIdPerson}/{id}";
            var persons = await _apiClient.GetAsync<PersonModel>(url);
            return PartialView(persons);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] PersonPost model)
        {
            bool isSuccess = await _apiClient.PutAsync(_api.EditPerson, model);
            if (!isSuccess)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while adding data.");
                return View(model);

            }
            return Ok(isSuccess);
        }
    }
}
