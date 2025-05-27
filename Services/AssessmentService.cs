using EduSyncAPI.DTOs;
using EduSyncAPI.Interfaces;
using EduSyncAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EduSyncAPI.Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly EduSyncDbContext _context;

        public AssessmentService(EduSyncDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Assessment>> GetAllAsync() =>
            await _context.Assessments.ToListAsync();

        public async Task<Assessment> GetByIdAsync(Guid id) =>
            await _context.Assessments.FindAsync(id);

        public async Task<Assessment> CreateAsync(AssessmentDto dto)
        {
            var assessment = new Assessment
            {
                AssessmentId = Guid.NewGuid(),
                CourseId = dto.CourseId,
                Title = dto.Title,
                Questions = dto.Questions,
                MaxScore = dto.MaxScore
            };
            _context.Assessments.Add(assessment);
            await _context.SaveChangesAsync();
            return assessment;
        }
    }

}
