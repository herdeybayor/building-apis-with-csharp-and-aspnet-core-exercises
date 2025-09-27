using Microsoft.EntityFrameworkCore;

namespace TheEmployeeAPI;

public class ApDbContext(DbContextOptions<ApDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasData(
            new Employee { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
            new Employee { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com" }
        );
    }
}
