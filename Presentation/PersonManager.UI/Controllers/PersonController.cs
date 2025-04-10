using Microsoft.AspNetCore.Mvc;
using PersonManager.UI.Helpers;
using PersonManager.UI.Models;
using System.Reflection;

namespace PersonManager.UI.Controllers
{
    [Route("persons")]
    public class PersonController : Controller
    {
        private readonly ApiClient _apiClient;
        public PersonController(ApiClient apiClient)
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
            var persons = await _apiClient.GetAsync<List<PersonResponse>>(ApiRoot.GetPersonList);
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
            bool isSuccess = await _apiClient.PostAsync(ApiRoot.PostPerson, model);
            if (!isSuccess)
            {
                ModelState.AddModelError(string.Empty, "Veri eklenirken bir hata oluştu.");
                return View(model);
                
            }
            return View(model);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var url = $"{ApiRoot.DeletePerson}/{id}";
            bool isSuccess = await _apiClient.DeleteAsync(url);
            return Ok(isSuccess);
        }
    }
}
