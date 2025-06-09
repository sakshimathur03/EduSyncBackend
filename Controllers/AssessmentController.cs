using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EduSyncAPI.Interfaces;
using EduSyncAPI.DTOs;

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
    public async Task<IActionResult> GetById(Guid id)
    {
        var assessment = await _assessmentService.GetByIdAsync(id);
        if (assessment == null)
            return NotFound($"Assessment with id {id} not found.");

        return Ok(assessment);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AssessmentDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var assessment = await _assessmentService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = assessment.AssessmentId }, assessment);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _assessmentService.DeleteAsync(id);
        if (!success)
            return NotFound($"Assessment with id {id} not found.");

        return NoContent();
    }
}
