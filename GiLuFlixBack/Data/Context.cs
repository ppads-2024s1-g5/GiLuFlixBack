using GiLuFlixBack.Models;
using Microsoft.EntityFrameworkCore;


namespace GiLuFlixBack.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<Filme> filme { get; set; }
}
