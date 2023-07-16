using AStar.Organisation.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AStar.Organisation.Infrastructure.DAL.Contexts.Configurations;

public class ProductPhotoConfiguration : IEntityTypeConfiguration<ProductPhoto>
{
    public void Configure(EntityTypeBuilder<ProductPhoto> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Url)
            .IsRequired();

        builder.Property(p => p.ProductId)
            .IsRequired();

        builder.HasOne(p => p.Product)
            .WithMany()
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}