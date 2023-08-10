using KUSYSDemoApp.Domain.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUSYSDemoApp.DataAccess.Mapping
{
    public class StudentCourseMap : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.CourseId).HasColumnType("varchar(10)");

            // Table & Field Mappings
            builder.ToTable("StudentCourses");
            builder.Property(t => t.Id).HasColumnName("Id");
            builder.Property(t => t.CourseId).HasColumnName("CourseId");
            builder.Property(t => t.StudentId).HasColumnName("StudentId");
            builder.Property(t => t.CreateDate).HasColumnName("CreateDate");

            // Relationships
            builder.HasOne(t => t.Course).WithMany(t => t.StudentCourses).HasForeignKey(d => d.CourseId);
            builder.HasOne(t => t.Student).WithMany(t => t.StudentCourses).HasForeignKey(d => d.StudentId);
        }
    }
}