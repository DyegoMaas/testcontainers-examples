using Microsoft.EntityFrameworkCore;

namespace WebAapi.Entities;

public class Customer
{
    public long Id { get; set; }
    public string Name { get; set; }
}

public class CustomerMapping
{
    public void Map(ModelBuilder modelBuilder)
    {
        var entityBuilder = modelBuilder.Entity<Customer>();
        entityBuilder.HasKey(x => x.Id);
        entityBuilder.Property(x => x.Name).HasColumnName("name");
    }
}