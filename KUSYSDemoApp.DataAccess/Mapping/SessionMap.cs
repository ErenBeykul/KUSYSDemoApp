using KUSYSDemoApp.Domain.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KUSYSDemoApp.DataAccess.Mapping
{
    public class SessionMap : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.UserAgent).HasColumnType("varchar(1000)");

            // Table & Field Mappings
            builder.ToTable("Sessions");
            builder.Property(t => t.Id).HasColumnName("Id");
            builder.Property(t => t.UserId).HasColumnName("UserId");
            builder.Property(t => t.UserAgent).HasColumnName("UserAgent");
            builder.Property(t => t.LoginTime).HasColumnName("LoginTime");

            // Relationships
            builder.HasOne(t => t.User).WithOne(t => t.Session).HasForeignKey<User>(d => d.LastSessionId);
        }
    }
}