using KUSYSDemoApp.Domain.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUSYSDemoApp.DataAccess.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Name).HasColumnType("varchar(50)");
            builder.Property(t => t.Surname).HasColumnType("varchar(50)");
            builder.Property(t => t.Username).HasColumnType("varchar(50)");
            builder.Property(t => t.Password).HasColumnType("varchar(100)");

            // Table & Field Mappings
            builder.ToTable("Users");
            builder.Property(t => t.Id).HasColumnName("Id");
            builder.Property(t => t.Name).HasColumnName("Name");
            builder.Property(t => t.Surname).HasColumnName("Surname");
            builder.Property(t => t.Username).HasColumnName("Username");
            builder.Property(t => t.Password).HasColumnName("Password");
            builder.Property(t => t.IsAdmin).HasColumnName("IsAdmin");
            builder.Property(t => t.StudentId).HasColumnName("StudentId");
            builder.Property(t => t.LastSessionId).HasColumnName("LastSessionId");
            builder.Property(t => t.LastLogin).HasColumnName("LastLogin");

            // Relationships
            builder.HasOne(t => t.Session).WithOne(t => t.User).HasForeignKey<Session>(d => d.UserId);
        }
    }
}