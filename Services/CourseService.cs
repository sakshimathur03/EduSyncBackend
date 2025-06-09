using EduSyncAPI.DTOs;
using EduSyncAPI.Interfaces;
using EduSyncAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EduSyncAPI.Services
{
    public class CourseService : ICourseService
    {
        private readonly EduSyncDbContext _context;

        public CourseService(EduSyncDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllAsync() =>
            await _context.Courses.ToListAsync();

        public async Task<Course> GetByIdAsync(Guid id) =>
            await _context.Courses.FindAsync(id);

        // ✅ Uses instructorId passed from controller (extracted from JWT)
        public async Task<Course> CreateCourseAsync(CourseDto dto, Guid instructorId)
        {
            var course = new Course
            {
                CourseId = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                MediaUrl = dto.MediaUrl,
                InstructorId = instructorId
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return course;
        }
        public async Task<bool> DeleteCourseAsync(Guid courseId, Guid instructorId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
                return false;

            if (course.InstructorId != instructorId)
                return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
