using AStar.Organisation.Core.Domain.Poco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AStar.Organisation.Infrastructure.DAL.Contexts.Configurations
{
    public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            builder.HasKey(e => e.Id).HasName("cartproduct_pkey");

            builder.ToTable("cartproduct");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.CartId).HasColumnName("cartid");
            builder.Property(e => e.ProductId).HasColumnName("productid");

            builder.HasOne(d => d.Cart).WithMany(p => p.CartProducts)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cartproduct_cartid_fkey");

            builder.HasOne(d => d.Product).WithMany(p => p.CartProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cartproduct_productid_fkey");
        }
    }
}