using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
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