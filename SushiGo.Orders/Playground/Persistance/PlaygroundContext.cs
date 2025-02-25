using Microsoft.EntityFrameworkCore;
using Playground.Entities;

namespace Playground.Persistance;

internal sealed class PlaygroundContext : DbContext
{
    public DbSet<Complaint> Complaints => Set<Complaint>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=PlaygroundDB;User Id=sa;Password=MyPassword1234!;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Complaint>()
            .ComplexProperty(c => c.Rma);
        
        base.OnModelCreating(modelBuilder);
    }
}