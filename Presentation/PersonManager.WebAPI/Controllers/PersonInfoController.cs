using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonManager.Application.Abstractions.PersonInfo;
using PersonManager.Application.Abstractions.PersonInfo.Contracts;
using System.Net.Mime;

namespace PersonManager.WebAPI.Controllers
{
    [Route("person-infos")]
    [Produces(MediaTypeNames.Application.Json)]
    public class PersonInfoController : ControllerBase
    {
        private readonly IPersonInfoService _personInfoService;
        private readonly IMapper _mapper;
        public PersonInfoController(IPersonInfoService personInfoService, IMapper mapper)
        {
            _personInfoService = personInfoService;
            _mapper = mapper;
        }

        [HttpGet("personId/{personId}")]
        public async Task<IActionResult> GetPersonIdInfo(Guid personId)
        {
            var personInfos = await _personInfoService.GetPersonIdInfoListAsync(personId);
            return Ok(personInfos);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PersonInfoRequestDto model)
        {
            var result = await _personInfoService.CreateAsync(model);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _personInfoService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
