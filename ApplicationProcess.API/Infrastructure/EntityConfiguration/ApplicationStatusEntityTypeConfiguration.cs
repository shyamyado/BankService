using ApplicationProcess.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationProcess.API.Infrastructure.Entityconfiguration
{
    public class ApplicationStatusEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationStatus>
    {
        public void Configure(EntityTypeBuilder<ApplicationStatus> builder)
        {
            builder.ToTable("ApplicationStatus");
            builder.Property(x => x.ApplicationId).IsRequired();
            builder.Property(x => x.StatusId).IsRequired();
            builder.Property(x => x.CreatedAt);
            builder.HasNoKey();
        }
    }
}
