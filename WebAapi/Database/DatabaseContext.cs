using System.Data.Entity.Infrastructure;
using Microsoft.EntityFrameworkCore;
using WebAapi.Entities;

namespace WebAapi.Database;

public class DatabaseContext : DbContext
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DatabaseContext()
    { }

    public DatabaseContext(DbContextOptions<DatabaseContext> options, IDbConnectionFactory connectionFactory) : base(options)
    {
        _connectionFactory = connectionFactory;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) 
            return;
        
        optionsBuilder.UseNpgsql(connection: _connectionFactory.CreateConnection(null));
    }
    
    public virtual DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        new CustomerMapping().Map(modelBuilder);
    }
}