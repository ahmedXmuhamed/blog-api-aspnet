using BlogApi.Models;
using Microsoft.EntityFrameworkCore;
namespace BlogApi.Data;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // user->post
        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u=>u.Post)
            .HasForeignKey(p => p.UserId);
       //  post->comment
       modelBuilder.Entity<Comment>()
           .HasOne(c => c.Post)
           .WithMany(p => p.Comments)
           .HasForeignKey(c => c.PostId);
       // user-> comment
        modelBuilder.Entity<Comment>()
           .HasOne(c => c.User)
           .WithMany(u => u.Comments)
           .HasForeignKey(c => c.UserId);
    }
}