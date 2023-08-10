using KUSYSDemoApp.Domain.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUSYSDemoApp.DataAccess.Mapping
{
    public class StudentMap : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Name).HasColumnType("varchar(100)");
            builder.Property(t => t.Surname).HasColumnType("varchar(100)");
            builder.Property(t => t.BirthDate).HasColumnType("date");

            // Table & Field Mappings
            builder.ToTable("Students");
            builder.Property(t => t.Id).HasColumnName("Id");
            builder.Property(t => t.Name).HasColumnName("Name");
            builder.Property(t => t.Surname).HasColumnName("Surname");
            builder.Property(t => t.BirthDate).HasColumnName("BirthDate");
            builder.Property(t => t.CreateDate).HasColumnName("CreateDate");
            builder.Property(t => t.UpdateDate).HasColumnName("UpdateDate");
            builder.Property(t => t.IsDeleted).HasColumnName("IsDeleted");

            // Relationships
            builder.HasMany(t => t.StudentCourses).WithOne(t => t.Student).HasForeignKey(d => d.StudentId);
        }
    }
}