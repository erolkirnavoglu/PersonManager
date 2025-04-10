using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonManager.Application.Abstractions.Person;
using PersonManager.Application.Abstractions.Person.Contracts;
using System.Net.Mime;

namespace PersonManager.WebAPI.Controllers
{
    [Route("persons")]
    [Produces(MediaTypeNames.Application.Json)]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        public PersonController(IPersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            var result = await _personService.GetListAsync();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PersonRequestDto model)
        {
            var result = await _personService.CreateAsync(model);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _personService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
