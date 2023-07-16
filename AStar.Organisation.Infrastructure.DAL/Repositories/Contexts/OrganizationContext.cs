﻿using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Infrastructure.DAL.Repositories.Contexts.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AStar.Organisation.Infrastructure.DAL.Repositories.Contexts
{
    public class OrganizationContext : DbContext
    {
        // public DbSet<Position> Positions { get; set; }
        // public DbSet<Department> Departments { get; set; }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductPhoto> ProductPhoto { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CartProduct> CartProduct { get; set; }
        public OrganizationContext() { }

        public OrganizationContext(DbContextOptions<OrganizationContext> options) 
            : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Position> ().ToTable ("Positions");
            // modelBuilder.ApplyConfiguration(new PositionConfiguration());
            //
            // modelBuilder.Entity<Department> ().ToTable ("Departments");
            // modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductPhotoConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CartProductConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}