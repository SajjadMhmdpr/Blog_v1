using Blog.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Blog.DataLayer.Context
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            #region Config Model And Relation

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasOne<User>(s => s.User)
                    .WithMany(g => g.Posts)
                    .HasForeignKey(s => s.UserId);

                entity.HasOne<Category>(s => s.Category)
                    .WithMany(g => g.Posts)
                    .HasForeignKey(s => s.CategoryId);

                entity.HasOne<Category>(s => s.SubCategory)
                    .WithMany(g => g.SubPosts)
                    .HasForeignKey(s => s.SubCategoryId);

                entity.Property(p => p.Title).HasMaxLength(100).IsRequired();
                entity.Property(p => p.Slug).HasMaxLength(100);
                entity.Property(p=>p.ImageName).HasMaxLength(150);
            });

            modelBuilder.Entity<PostComment>(entity =>
            {
                entity.HasOne<Post>(s => s.Post)
                    .WithMany(g => g.PostComments)
                    .HasForeignKey(s => s.PostId);

                entity.HasOne<User>(s => s.User)
                    .WithMany(g => g.PostComments)
                    .HasForeignKey(s => s.UserId);

                entity.Property(p => p.Text).HasMaxLength(1000).IsRequired();

            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(p => p.MetaDescription).HasMaxLength(50);
                entity.Property(p => p.MetaTag).HasMaxLength(50);
                entity.Property(p => p.Title).HasMaxLength(100).IsRequired();

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(p => p.FullName).HasMaxLength(100).IsRequired();
                entity.Property(p => p.Password).HasMaxLength(50);
                entity.Property(p => p.UserName).HasMaxLength(50);
            });

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            #endregion


        }
    }
}
