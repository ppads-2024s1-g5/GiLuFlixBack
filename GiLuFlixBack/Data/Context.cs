using GiLuFlixBack.Models;
using Microsoft.EntityFrameworkCore;


namespace GiLuFlixBack.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>().Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Book>().Property(p => p.Id).UseIdentityColumn(); 
        modelBuilder.Entity<TvShow>().Property(p => p.Id).UseIdentityColumn(); 
        modelBuilder.Entity<User>().Property(p => p.Id).UseIdentityColumn(); 
        modelBuilder.Entity<Review>().Property(p => p.ReviewId).UseIdentityColumn(); 
    }

    public DbSet<Movie> movie { get; set; }
    public DbSet<Book> book { get; set; }
    public DbSet<User> user { get; set; }
    public DbSet<Review> review { get; set; }
    public DbSet<TvShow> tvShow { get; set; }

}
