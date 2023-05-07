using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 3,
                    Name = "Marry",
                    Department = Dept.IT,
                    Email = "marry@pragimtech.com"
                },
                new Employee
                {
                    Id = 5,
                    Name = "John",
                    Department = Dept.IT,
                    Email = "John@pragimtech.com"
                }
            );
        }
    }
}
