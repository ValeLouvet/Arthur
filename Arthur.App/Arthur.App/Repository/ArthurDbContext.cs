using Arthur.App.Domain;
using Microsoft.EntityFrameworkCore;

namespace Arthur.App.Repository;

public class ArthurDbContext : DbContext
{
    public ArthurDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Stats> Stats { get; set; }
}
