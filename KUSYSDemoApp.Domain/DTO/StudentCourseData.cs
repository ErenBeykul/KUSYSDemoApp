namespace KUSYSDemoApp.Domain.DTO
{
    public class StudentCourseData
    {
        public Guid Id { get; set; }
        public string? Student { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }
        public string? CreateDate { get; set; }
        public List<string> CourseIds { get; set; } = new();
    }
}