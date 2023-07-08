﻿using AStar.Domain.Entities;
using AStar.Organisation.Infrastructure.DAL.Repositories.Contexts.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AStar.Organisation.Infrastructure.DAL.Repositories.Contexts
{
    public class OrganizationContext : DbContext
    {
        public DbSet<Position> Positions { get; set; }

        public OrganizationContext() { }

        public OrganizationContext(DbContextOptions<OrganizationContext> options) 
            : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Position> ().ToTable ("Positions");
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}