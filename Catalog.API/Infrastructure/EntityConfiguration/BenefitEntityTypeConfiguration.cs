using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.API.Infrastructure.EntityConfiguration
{
    public class BenefitEntityTypeConfiguration : IEntityTypeConfiguration<Benefit>
    {
        public void Configure(EntityTypeBuilder<Benefit> builder)
        {
            builder.ToTable("Benefits");
            builder.HasKey(x => x.Id).IsClustered();
            builder.HasOne(x => x.Item)
              .WithMany()
              .HasForeignKey(x => x.ItemId);
            builder.Property(x => x.Benefits);
        }
    }
}