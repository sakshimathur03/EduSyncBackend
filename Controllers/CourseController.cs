using System.Security.Claims;
using EduSyncAPI.DTOs;
using EduSyncAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetAll() =>
        Ok(await _courseService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) =>
        Ok(await _courseService.GetByIdAsync(id));

    // 🔐 Require Instructor role
    [HttpPost]
    [Authorize(Roles = "Instructor")]
    public async Task<IActionResult> Create([FromBody] CourseDto dto)
    {
        // ✅ Extract instructor ID from token
        var instructorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (instructorIdClaim == null)
            return Unauthorized("Invalid token — user ID missing");

        var instructorId = Guid.Parse(instructorIdClaim.Value);

        var course = await _courseService.CreateCourseAsync(dto, instructorId);
        return CreatedAtAction(nameof(GetById), new { id = course.CourseId }, course);
    }
}
