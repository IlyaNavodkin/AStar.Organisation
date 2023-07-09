using AStar.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AStar.Organisation.Infrastructure.DAL.Repositories.Contexts.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired();
        builder.HasIndex(p => p.Name);
        
        builder.HasOne(p => p.Department) 
            .WithMany(d => d.Positions)
            .HasForeignKey(p => p.DepartmentId); 
    }
}