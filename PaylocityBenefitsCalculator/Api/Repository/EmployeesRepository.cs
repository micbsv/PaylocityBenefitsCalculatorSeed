using Api.Models;
using Api.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class EmployeesRepository : IEmployeesRepository
    {
        static EmployeesRepository()
        {
            // Populate the database with sample data
            var employees = new List<Employee>
            {
                new()
                {
                    Id = 1,
                    FirstName = "LeBron",
                    LastName = "James",
                    Salary = 75420.99m,
                    DateOfBirth = new DateTime(1984, 12, 30)
                },
                new()
                {
                    Id = 2,
                    FirstName = "Ja",
                    LastName = "Morant",
                    Salary = 92365.22m,
                    DateOfBirth = new DateTime(1999, 8, 10),
                    Dependents = new List<Dependent>
                    {
                        new()
                        {
                            Id = 1,
                            FirstName = "Spouse",
                            LastName = "Morant",
                            Relationship = Relationship.Spouse,
                            DateOfBirth = new DateTime(1998, 3, 3)
                        },
                        new()
                        {
                            Id = 2,
                            FirstName = "Child1",
                            LastName = "Morant",
                            Relationship = Relationship.Child,
                            DateOfBirth = new DateTime(2020, 6, 23)
                        },
                        new()
                        {
                            Id = 3,
                            FirstName = "Child2",
                            LastName = "Morant",
                            Relationship = Relationship.Child,
                            DateOfBirth = new DateTime(2021, 5, 18)
                        }
                    }
                },
                new()
                {
                    Id = 3,
                    FirstName = "Michael",
                    LastName = "Jordan",
                    Salary = 143211.12m,
                    DateOfBirth = new DateTime(1963, 2, 17),
                    Dependents = new List<Dependent>
                    {
                        new()
                        {
                            Id = 4,
                            FirstName = "DP",
                            LastName = "Jordan",
                            Relationship = Relationship.DomesticPartner,
                            DateOfBirth = new DateTime(1974, 1, 2)
                        }
                    }
                }
            };

            using var context = new ApiDbContext();
            context.Employees.AddRange(employees);
            context.SaveChanges();
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            using var context = new ApiDbContext();
            var list = await context.Employees
                .Include(e => e.Dependents)
                .ToListAsync();

            return list;
        }

        public async Task<Employee?> GetAsync(int employeeId)
        {
            using var context = new ApiDbContext();
            var employee = await context.Employees
                .Include(e => e.Dependents)
                .SingleOrDefaultAsync(e => e.Id == employeeId);

            return employee;
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            using var context = new ApiDbContext();
            var employeeEntity = await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();

            return employeeEntity.Entity;
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            using var context = new ApiDbContext();
            var employeeEntity = context.Employees.Update(employee);
            await context.SaveChangesAsync();

            return employeeEntity.Entity;
        }

        public async Task<Employee> DeleteAsync(Employee employee)
        {
            using var context = new ApiDbContext();
            var employeeEntity = context.Employees.Remove(employee);
            await context.SaveChangesAsync();

            return employeeEntity.Entity;
        }
    }
}
