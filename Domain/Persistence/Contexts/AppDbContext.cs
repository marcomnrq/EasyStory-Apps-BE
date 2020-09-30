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
        public DbSet<PostHashtag> PostHashtags { get; set; }
        public DbSet<Hashtag>Hashtags { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //User Entity
            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.Username).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(p => p.Password).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(p => p.Email).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(p => p.AccountBalance).IsRequired();
            builder.Entity<User>().Property(p => p.SubscriptionPrice).IsRequired();
            builder.Entity<User>().HasMany(p => p.Posts).WithOne(p => p.User).HasForeignKey(p =>p.UserId);
            builder.Entity<User>().HasMany(p => p.Bookmarks).WithOne(p => p.User).HasForeignKey(p => p.UserId);
            
            //Comment Entity
            builder.Entity<Comment>().ToTable("Comments");
            builder.Entity<Comment>().HasKey(p => p.Id);
            builder.Entity<Comment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Comment>().Property(p => p.Content).IsRequired().HasMaxLength(30);
            builder.Entity<Comment>().Property(p => p.PostId).IsRequired();
            builder.Entity<Comment>().Property(p => p.UserId).IsRequired();

            //BookMark Entity
            builder.Entity<Bookmark>().ToTable("Bookmarks");
            builder.Entity<Bookmark>().HasKey(p => p.Id);
            builder.Entity<Bookmark>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Comment>().Property(p => p.UserId).IsRequired();

            // Post Entity
            builder.Entity<Post>().ToTable("Posts");
            builder.Entity<Post>().HasKey(p => p.Id);
            builder.Entity<Post>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Post>().Property(p => p.Title)
                .IsRequired().HasMaxLength(30);
            builder.Entity<Post>().Property(p => p.Description)
                .IsRequired().HasMaxLength(60);
            builder.Entity<Post>().Property(p => p.Content)
                .IsRequired().HasMaxLength(80);
            builder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(p => p.Post)
                .HasForeignKey(p => p.PostId);
            
                                  


            // Hashtag Entity
            builder.Entity<Hashtag>().ToTable("Hashtags");
            builder.Entity<Hashtag>().HasKey(p => p.Id);
            builder.Entity<Hashtag>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Hashtag>().Property(p => p.Name).IsRequired().HasMaxLength(30);

            // PostHashtag Entity
            builder.Entity<PostHashtag>().ToTable("PostHashtags");
            builder.Entity<PostHashtag>().HasKey(pt => new { pt.PostId, pt.HashtagId });

            builder.Entity<PostHashtag>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostHashtags)
                .HasForeignKey(pt => pt.PostId);

            builder.Entity<PostHashtag>()
                .HasOne(pt => pt.Hashtag)
                .WithMany(t => t.PostHashtags)
                .HasForeignKey(pt => pt.HashtagId);
            
            // Naming convention Policy
            builder.ApplySnakeCaseNamingConvention();
        }
    }
}
