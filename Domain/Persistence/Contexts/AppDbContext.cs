using System;
using Microsoft.EntityFrameworkCore;
using EasyStory.API.Domain.Models;
using EasyStory.API.Extensions;

namespace EasyStory.API.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Category Entity
            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<Category>().HasKey(p => p.Id);
            builder.Entity<Category>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Category>().Property(p => p.Name)
                .IsRequired().HasMaxLength(30);
            builder.Entity<Category>()
                .HasMany(p => p.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);
            builder.Entity<Category>().HasData
                (
                new Category { Id = 100, Name = "Fruits and Vegetables" },
                new Category { Id = 101, Name = "Dairy" }
                );

            // Product Entity
            builder.Entity<Product>().ToTable("Products");
            builder.Entity<Product>().HasKey(p => p.Id);
            builder.Entity<Product>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Product>().Property(p => p.Name)
                .IsRequired().HasMaxLength(50);
            builder.Entity<Product>().Property(p => p.QuantityInPackage)
                .IsRequired();
            builder.Entity<Product>().Property(p => p.UnitOfMeasurement)
                .IsRequired();
            builder.Entity<Product>().HasData
                (
                new Product
                { Id = 100, Name = "Apple", QuantityInPackage = 1, UnitOfMeasurement = EUnitOfMeasurement.Unity, CategoryId = 100 },
                new Product
                { Id = 101, Name = "Milk", QuantityInPackage = 2, UnitOfMeasurement = EUnitOfMeasurement.Liter, CategoryId = 101 }
                );

            // Tag Entity
            builder.Entity<Tag>().ToTable("Tags");
            builder.Entity<Tag>().HasKey(p => p.Id);
            builder.Entity<Tag>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Tag>().Property(p => p.Name).IsRequired().HasMaxLength(30);

            // ProductTag Entity
            builder.Entity<ProductTag>().ToTable("ProductTags");
            builder.Entity<ProductTag>().HasKey(pt => new { pt.ProductId, pt.TagId });

            builder.Entity<ProductTag>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.ProductTags)
                .HasForeignKey(pt => pt.ProductId);

            builder.Entity<ProductTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.ProductTags)
                .HasForeignKey(pt => pt.TagId);


            // Naming convention Policy
            builder.ApplySnakeCaseNamingConvention();
        }
    }
}
