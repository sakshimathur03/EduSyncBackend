using EduSyncAPI.DTOs;
using EduSyncAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]

    [AllowAnonymous]
    public async Task<IActionResult> GetAll() =>
        Ok(await _courseService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) =>
        Ok(await _courseService.GetByIdAsync(id));

    [HttpPost]
    [Authorize(Roles = "Instructor")]
    public async Task<IActionResult> Create([FromBody] CourseDto dto)
    {
        var instructorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (instructorIdClaim == null)
            return Unauthorized("Invalid token — user ID missing");

        var instructorId = Guid.Parse(instructorIdClaim.Value);

        var course = await _courseService.CreateCourseAsync(dto, instructorId);
        return CreatedAtAction(nameof(GetById), new { id = course.CourseId }, course);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Instructor")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var instructorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (instructorIdClaim == null)
            return Unauthorized("Invalid token — user ID missing");

        var instructorId = Guid.Parse(instructorIdClaim.Value);

        var success = await _courseService.DeleteCourseAsync(id, instructorId);

        if (!success)
            return NotFound("Course not found or you do not have permission to delete it.");

        return NoContent();
    }
}
