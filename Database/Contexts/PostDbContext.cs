using System;
using BlogApi.Database.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using PostApi.Models;
namespace PostApi.Database.Contexts
{
    public class PostDbContext : DbContext, IDbContextSchema
    {
        public string Schema { get; }
        public PostDbContext(DbContextOptions<PostDbContext> options, IDbContextSchema schema = null) : base(options)
        {
            Schema = schema.Schema ?? throw new ArgumentNullException(nameof(schema));
        }

        // Table objects
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ReplaceService<IModelCacheKeyFactory, DbSchemaAwareModelCacheKeyFactory>();
            optionsBuilder.ReplaceService<IMigrationsAssembly, DbSchemaAwareMigrationAssembly>();

            base.OnConfiguring(optionsBuilder);
        }

        // Alternative way to define your table, relationship, constraints etc..
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.HasDefaultSchema(Schema);  //Required for dynamic schema change
            // Post Table
            modelBuilder.Entity<Post>(
                p =>
                {
                    p.Property(x => x.Id).HasDefaultValueSql("uuid_generate_v4()");
                }
            );

            // Category Table
            modelBuilder.Entity<Category>(
                p =>
                {
                    p.Property(x => x.Id).HasDefaultValueSql("uuid_generate_v4()");
                }
            );
            //PostCategory Table (Many to many relation)
            modelBuilder.Entity<PostCategory>().HasKey(pc => new { pc.PostId, pc.CategoryId });
            modelBuilder.Entity<PostCategory>()
                .HasOne<Post>(pc => pc.Post)
                .WithMany(p => p.PostCategories)
                .HasForeignKey(pc => pc.PostId);

            modelBuilder.Entity<PostCategory>()
                .HasOne<Category>(pc => pc.Category)
                .WithMany(c => c.PostCategories)
                .HasForeignKey(pc => pc.CategoryId);

        }
    }
}