using AStar.Organisation.Core.Domain.Poco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AStar.Organisation.Infrastructure.DAL.Contexts.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.Id).HasName("customer_pkey");

            builder.ToTable("customer");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            builder.Property(e => e.Phone).HasColumnName("phone");
        }
    }
}