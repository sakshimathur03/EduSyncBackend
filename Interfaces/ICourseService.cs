using EduSyncAPI.DTOs;
using EduSyncAPI.Models;

namespace EduSyncAPI.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> GetByIdAsync(Guid id);

        // ✅ Updated to take instructorId from the controller
        Task<Course> CreateCourseAsync(CourseDto dto, Guid instructorId);
    }
}