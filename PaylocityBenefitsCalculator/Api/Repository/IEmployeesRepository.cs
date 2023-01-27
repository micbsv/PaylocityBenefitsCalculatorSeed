using Api.Models;

namespace Api.Repository
{
    public interface IEmployeesRepository
    {
        Task<Employee> AddAsync(Employee employee);
        Task<List<Employee>> GetAllAsync();
        Task<Employee?> GetAsync(int employeeId);
        Task<Employee> UpdateAsync(Employee employee);
        Task<Employee> DeleteAsync(Employee employee);
    }
}
