using KUSYSDemoApp.Domain.DBModels;
using Microsoft.EntityFrameworkCore;

namespace KUSYSDemoApp.DataAccess
{
    public interface IKUSYSDemoAppContext
    {        
        DbSet<Course> Courses { get; set; }
        DbSet<Session> Sessions { get; set; }
        DbSet<StudentCourse> StudentCourses { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<User> Users { get; set; }
        int SaveChanges();
    }
}