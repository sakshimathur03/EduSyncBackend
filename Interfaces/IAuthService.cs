using EduSyncAPI.DTOs;
using EduSyncAPI.Models;

namespace EduSyncAPI.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<User> RegisterAsync(UserDto dto);
    }

}
