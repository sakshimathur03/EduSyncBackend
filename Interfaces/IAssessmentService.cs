using EduSyncAPI.DTOs;
using EduSyncAPI.Models;

public interface IAssessmentService
{
    Task<IEnumerable<Assessment>> GetAllAsync();
    Task<Assessment> GetByIdAsync(Guid id);
    Task<Assessment> CreateAsync(AssessmentDto dto);
    Task<bool> DeleteAsync(Guid id);
}
