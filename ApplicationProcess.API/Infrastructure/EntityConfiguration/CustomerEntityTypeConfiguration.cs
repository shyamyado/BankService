using ApplicationProcess.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationProcess.API.Infrastructure.Entityconfiguration
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(x => x.Id).IsClustered();
            builder.Property(builder=> builder.Name).IsRequired();
            builder.Property(builder => builder.Phone).IsRequired();
            builder.Property(builder => builder.Email).IsRequired();
            builder.Property(builder => builder.DOB).HasColumnType("datetime2").IsRequired(false);
            builder.Property(builder => builder.Address).IsRequired(false);
            builder.Property(builder => builder.City).IsRequired(false);
            builder.Property(builder => builder.IsActive); ;
            builder.Property(builder => builder.CreatedAt);
            builder.Property(builder => builder.UpdatedAt);


        }
    }
}
