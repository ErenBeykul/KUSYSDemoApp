using KUSYSDemoApp.Domain.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUSYSDemoApp.DataAccess.Mapping
{
    public class CourseMap : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id).HasColumnType("varchar(10)");
            builder.Property(t => t.Name).HasColumnType("varchar(20)");

            // Table & Field Mappings
            builder.ToTable("Courses");
            builder.Property(t => t.Id).HasColumnName("Id");
            builder.Property(t => t.Name).HasColumnName("Name");
            builder.Property(t => t.CreateDate).HasColumnName("CreateDate");
            builder.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            builder.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            // Relationships
            builder.HasMany(t => t.StudentCourses).WithOne(t => t.Course).HasForeignKey(d => d.CourseId);
        }
    }
}