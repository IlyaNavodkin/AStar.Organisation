using AStar.Organisation.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AStar.Organisation.Infrastructure.DAL.Contexts.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(p => p.Email)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(p => p.Phone)
            .HasMaxLength(30)
            .IsRequired();
    }
}