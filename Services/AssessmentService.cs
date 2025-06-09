using System.Text.Json;
using EduSyncAPI.DTOs;
using EduSyncAPI.Helpers;
using EduSyncAPI.Interfaces;
using EduSyncAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EduSyncAPI.Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly EduSyncDbContext _context;
        private readonly EventHubPublisher _eventHubPublisher;

        public AssessmentService(EduSyncDbContext context, EventHubPublisher eventHubPublisher)
        {
            _context = context;
            _eventHubPublisher = eventHubPublisher;
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
                Questions = JsonSerializer.Serialize(dto.Questions),
                MaxScore = dto.MaxScore
            };

            _context.Assessments.Add(assessment);
            await _context.SaveChangesAsync();

            return assessment;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var assessment = await _context.Assessments.FindAsync(id);
            if (assessment == null)
                return false;

            _context.Assessments.Remove(assessment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
