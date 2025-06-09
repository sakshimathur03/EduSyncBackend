using System.Text.Json.Serialization;

namespace EduSyncAPI.DTOs
{
    public class QuestionDto
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public string Answer { get; set; }
    }

    public class AssessmentDto
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public List<QuestionDto> Questions { get; set; }  // change to List<QuestionDto>
        public int MaxScore { get; set; }
    }
}
