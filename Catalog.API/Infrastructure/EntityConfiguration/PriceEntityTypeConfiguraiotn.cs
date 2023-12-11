using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Catalog.API.Models;

namespace Catalog.API.Infrastructure.EntityConfiguration
{
    public class PriceEntityTypeConfiguraiotn : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder.ToTable("Price");
            builder.HasKey(x => x.Id).IsClustered();
            builder.HasOne(x => x.Item)
              .WithMany()
              .HasForeignKey(x => x.ItemId);
            builder.Property(x => x.Amount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x => x.WithdrawlLimit).HasColumnType("decimal(18,2)");
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.CreatedAt);
        }
    }
}
