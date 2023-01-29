using Api.Dtos.Employee;
using Api.PaycheckCalculator;

namespace Api.Services
{
    public interface IEmployeesService
    {
        Task<List<GetEmployeeDto>> GetAllAsync();
        Task<GetEmployeeDto?> GetAsync(int employeeId);
        Task<GetEmployeeDto> AddAsync(AddEmployeeDto employee);
        Task<GetEmployeeDto> UpdateAsync(int employeeId, UpdateEmployeeDto updatedEmployee);
        Task<GetEmployeeDto> DeleteAsync(int employeeId);
    }
}
