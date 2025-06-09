using EduSyncAPI.DTOs;
using EduSyncAPI.Helpers;
using EduSyncAPI.Interfaces;
using EduSyncAPI.Models;
using Microsoft.EntityFrameworkCore;

public class ResultService : IResultService
{
    private readonly EduSyncDbContext _context;
    private readonly EventHubPublisher _eventHubPublisher;

    public ResultService(EduSyncDbContext context, EventHubPublisher eventHubPublisher)
    {
        _context = context;
        _eventHubPublisher = eventHubPublisher;
    }

    public async Task<Result> CreateAsync(ResultDto dto)
    {
        if (dto.AssessmentId == Guid.Empty || dto.UserId == Guid.Empty)
            throw new ArgumentException("Invalid GUIDs in request");

        var assessmentExists = await _context.Assessments.AnyAsync(a => a.AssessmentId == dto.AssessmentId);
        if (!assessmentExists)
            throw new ArgumentException("Assessment does not exist");

        var userExists = await _context.Users.AnyAsync(u => u.UserId == dto.UserId);
        if (!userExists)
            throw new ArgumentException("User does not exist");

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

        await _eventHubPublisher.PublishAsync(new
        {
            EventType = "QuizSubmitted",
            result.ResultId,
            result.AssessmentId,
            result.UserId,
            result.Score,
            result.AttemptDate
        });

        return result;
    }

    public async Task<IEnumerable<Result>> GetAllAsync() => await _context.Results.ToListAsync();

    public async Task<Result> GetByIdAsync(Guid id) => await _context.Results.FindAsync(id);
}
