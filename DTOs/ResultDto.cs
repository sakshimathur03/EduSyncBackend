namespace EduSyncAPI.DTOs
{
    public class ResultDto
    {
        public Guid AssessmentId { get; set; }
        public Guid UserId { get; set; }
        public int Score { get; set; }
    }

}
