using Catalog.API.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.API.Infrastructure.EntityConfiguration
{
    public class ItemEntityTypeConfiguration : IEntityTypeConfiguration<ItemViewModel>
    {
        public void Configure(EntityTypeBuilder<ItemViewModel> builder)
        {
            builder.ToTable("Item");
            builder.HasKey(x => x.Id).IsClustered();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description);
            builder.Property(x => x.ShortDescription);
            builder.Property(x => x.JoiningFees).HasColumnType("decimal(18,2)");
            builder.Property(x => x.AnnualFees).HasColumnType("decimal(18,2)");
            builder.HasOne(ci => ci.Category)
              .WithMany()
              .HasForeignKey(ci => ci.CategoryId);
            builder.Property(x => x.IsActive);
            builder.Property(x => x.IsPhysical);
            builder.Property(x => x.Image);
            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.UpdatedAt);
        }
    }
}
