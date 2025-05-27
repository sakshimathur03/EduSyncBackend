using EduSyncAPI.DTOs;
using EduSyncAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduSyncAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _userService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id) => Ok(await _userService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto dto)
        {
            var result = await _userService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.UserId }, result);
        }
    }

}
