using AStar.Organisation.Core.Domain.Poco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AStar.Organisation.Infrastructure.DAL.Contexts.Configurations
{
    public class ProductPhotoConfiguration : IEntityTypeConfiguration<ProductPhoto>
    {
        public void Configure(EntityTypeBuilder<ProductPhoto> builder)
        {
            builder.HasKey(e => e.Id).HasName("productphoto_pkey");

            builder.ToTable("productphoto");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ProductId).HasColumnName("productid");
            builder.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("url");

            builder.HasOne(d => d.Product).WithMany(p => p.ProductPhotos)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("productphoto_productid_fkey");
        }
    }
}