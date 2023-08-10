namespace KUSYSDemoApp.Domain.DBModels
{
    public class Course
    {
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<StudentCourse> StudentCourses { get; } = new List<StudentCourse>();
    }
}