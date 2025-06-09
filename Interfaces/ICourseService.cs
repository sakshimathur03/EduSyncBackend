using EduSyncAPI.DTOs;
using EduSyncAPI.Models;

namespace EduSyncAPI.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> GetByIdAsync(Guid id);
        Task<Course> CreateCourseAsync(CourseDto dto, Guid instructorId);

        // Delete course method
        Task<bool> DeleteCourseAsync(Guid courseId, Guid instructorId);
    }

}