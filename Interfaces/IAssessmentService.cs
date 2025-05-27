using EduSyncAPI.DTOs;
using EduSyncAPI.Models;

namespace EduSyncAPI.Interfaces
{
    public interface IAssessmentService
    {
        Task<IEnumerable<Assessment>> GetAllAsync();
        Task<Assessment> GetByIdAsync(Guid id);
        Task<Assessment> CreateAsync(AssessmentDto dto);
    }

}
