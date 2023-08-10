namespace KUSYSDemoApp.Domain.DBModels
{
    public class StudentCourse
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public DateTime CreateDate { get; set; }
        public string CourseId { get; set; } = string.Empty;

        public virtual Course? Course { get; set; }
        public virtual Student? Student { get; set; }
    }
}