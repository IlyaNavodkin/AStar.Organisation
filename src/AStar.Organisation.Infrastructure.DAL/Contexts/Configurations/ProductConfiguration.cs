using AStar.Organisation.Core.Domain.Poco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AStar.Organisation.Infrastructure.DAL.Contexts.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id).HasName("product_pkey");

            builder.ToTable("product");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            
            builder.Property(e => e.Price).HasColumnName("price");
        }
    }
}