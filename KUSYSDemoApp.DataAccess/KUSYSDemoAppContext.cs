using KUSYSDemoApp.DataAccess.Mapping;
using KUSYSDemoApp.Domain.DBModels;
using Microsoft.EntityFrameworkCore;

namespace KUSYSDemoApp.DataAccess
{
    public partial class KUSYSDemoAppContext : DbContext, IKUSYSDemoAppContext
    {
        public KUSYSDemoAppContext(DbContextOptions<KUSYSDemoAppContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            Database.EnsureCreated();
        }

        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Session> Sessions { get; set; } = null!;
        public virtual DbSet<StudentCourse> StudentCourses { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            ModelBuilder.ApplyConfiguration(new CourseMap());
            ModelBuilder.ApplyConfiguration(new SessionMap());
            ModelBuilder.ApplyConfiguration(new StudentCourseMap());
            ModelBuilder.ApplyConfiguration(new StudentMap());
            ModelBuilder.ApplyConfiguration(new UserMap());

            foreach (var relationship in ModelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}