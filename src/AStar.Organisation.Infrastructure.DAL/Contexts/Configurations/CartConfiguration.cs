using AStar.Organisation.Core.Domain.Poco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AStar.Organisation.Infrastructure.DAL.Contexts.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(e => e.Id).HasName("cart_pkey");

            builder.ToTable("cart");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.CustomerId).HasColumnName("customerid");

            builder.HasOne(d => d.Customer).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cart_customerid_fkey");
        }
    }
}