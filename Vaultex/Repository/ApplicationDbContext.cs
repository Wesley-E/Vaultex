using Microsoft.EntityFrameworkCore;
using Vaultex.Models;

namespace Vaultex.Repository;

public class ApplicationDbContext : DbContext
{
    private readonly string _connectionString;

    public ApplicationDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .Property(e => e.EmployeeId)
            .HasDefaultValueSql("gen_random_uuid()");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }
    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Organisation> Organisations { get; set; }
}