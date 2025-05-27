using EduSyncAPI.DTOs;
using EduSyncAPI.Interfaces;
using EduSyncAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EduSyncAPI.Services
{
    public class ResultService : IResultService
    {
        private readonly EduSyncDbContext _context;

        public ResultService(EduSyncDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Result>> GetAllAsync() =>
            await _context.Results.ToListAsync();

        public async Task<Result> GetByIdAsync(Guid id) =>
            await _context.Results.FindAsync(id);

        public async Task<Result> CreateAsync(ResultDto dto)
        {
            var result = new Result
            {
                ResultId = Guid.NewGuid(),
                AssessmentId = dto.AssessmentId,
                UserId = dto.UserId,
                Score = dto.Score,
                AttemptDate = DateTime.UtcNow
            };
            _context.Results.Add(result);
            await _context.SaveChangesAsync();
            return result;
        }
    }

}
