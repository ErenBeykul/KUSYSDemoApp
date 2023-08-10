namespace KUSYSDemoApp.Domain.DBModels
{
    public class Student
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;

        public virtual ICollection<StudentCourse> StudentCourses { get; } = new List<StudentCourse>();
    }
}