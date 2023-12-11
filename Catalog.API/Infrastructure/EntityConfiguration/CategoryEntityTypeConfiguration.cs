using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.API.Infrastructure.EntityConfiguration
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.ToTable("Category");
            builder.HasKey(x => x.Id).IsClustered();
            builder.Property(x => x.CategoryName);
            builder.Property(x => x.Subcategory);
            builder.HasOne(ci => ci.Department)
              .WithMany()
              .HasForeignKey(ci => ci.DepartmentId);
            builder.Property(x => x.DepartmentId);
            builder.Property(x => x.Type);
            builder.Property(x => x.IsActive);
            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.UpdatedAt);


        }
    }
}
