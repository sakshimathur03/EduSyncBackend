using EduSyncAPI.DTOs;
using EduSyncAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduSyncAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssessmentsController : ControllerBase
    {
        private readonly IAssessmentService _assessmentService;

        public AssessmentsController(IAssessmentService assessmentService)
        {
            _assessmentService = assessmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _assessmentService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) =>
            Ok(await _assessmentService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AssessmentDto dto)
        {
            var assessment = await _assessmentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = assessment.AssessmentId }, assessment);
        }
    }

}
