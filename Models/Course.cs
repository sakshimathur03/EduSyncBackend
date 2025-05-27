namespace EduSyncAPI.Models
{
    public class Course
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid InstructorId { get; set; }
        public string MediaUrl { get; set; }
    }

}
