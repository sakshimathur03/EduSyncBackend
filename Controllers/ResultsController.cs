using EduSyncAPI.DTOs;
using EduSyncAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduSyncAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly IResultService _resultService;

        public ResultsController(IResultService resultService)
        {
            _resultService = resultService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _resultService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) =>
            Ok(await _resultService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ResultDto dto)
        {
            var result = await _resultService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.ResultId }, result);
        }
    }

}
