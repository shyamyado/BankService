using Microsoft.EntityFrameworkCore;
using Catalog.API.Infrastructure.EntityConfiguration;
using Catalog.API.Models;
using Catalog.API.ViewModels;

namespace Catalog.API.Infrastructure
{
    public class CatalogDBContext : DbContext
    {
        private const string ConnectionString = "Server=SHYAMASUS\\SQLEXPRESS;Initial Catalog=OfferCatalog;Integrated Security=true;TrustServerCertificate=True;";

        public CatalogDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Benefit> Benefits { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ItemViewModel> Items { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BenefitEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentEnityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PriceEntityTypeConfiguraiotn());
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
