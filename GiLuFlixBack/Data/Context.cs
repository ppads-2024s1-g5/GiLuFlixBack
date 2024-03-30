using GiLuFlixBack.Models;
using Microsoft.EntityFrameworkCore;


namespace GiLuFlixBack.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Filme>().Property(p => p.Id).UseIdentityColumn();
    }

    public DbSet<Filme> filme { get; set; }
}
