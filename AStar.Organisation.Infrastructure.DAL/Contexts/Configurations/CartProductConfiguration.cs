using AStar.Organisation.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AStar.Organisation.Infrastructure.DAL.Contexts.Configurations;

public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
{
    public void Configure(EntityTypeBuilder<CartProduct> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.CartId)
            .IsRequired();
        
        builder.HasOne(p => p.Cart)
            .WithMany()
            .HasForeignKey(p => p.CartId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.Property(p => p.ProductId)
            .IsRequired();
        
        builder.HasOne(p => p.Product)
            .WithMany()
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}