using Api.Dtos.Employee;
using Api.Mappers;
using Api.Models;
using Api.Repository;
using System.Data;

namespace Api.Services
{
    // This class provides abstraction from Repository and handles mapping db entities to dtos
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _employeesRepo;

        public EmployeesService(IEmployeesRepository employeesRepo)
        {
            _employeesRepo = employeesRepo;
        }

        public async Task<List<GetEmployeeDto>> GetAllAsync()
        {
            var employees = await _employeesRepo.GetAllAsync();
            var employeeDtos = ApiMapper.Map<Employee, GetEmployeeDto>(employees.AsQueryable());

            var list = employeeDtos.ToList();
            return list;
        }

        public async Task<GetEmployeeDto?> GetAsync(int employeeId)
        {
            var employee = await GetEmployeeAsync(employeeId);
            if (employee == null)
                return null;

            var employeeDto = ApiMapper.Map<Employee, GetEmployeeDto>(employee);
            return employeeDto;
        }

        public async Task<GetEmployeeDto> AddAsync(AddEmployeeDto employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            var employeeModel = ApiMapper.Map<AddEmployeeDto, Employee>(employee);
            employeeModel = await _employeesRepo.AddAsync(employeeModel);
            
            var employeeDto = ApiMapper.Map<Employee, GetEmployeeDto>(employeeModel);
            return employeeDto;
        }

        public async Task<GetEmployeeDto> UpdateAsync(int employeeId, UpdateEmployeeDto updatedEmployee)
        {
            var employee = await GetEmployeeAsync(employeeId);
            if (employee == null)
                throw new NoNullAllowedException($"There is no employee with id: {employeeId}");

            employee.FirstName = updatedEmployee.FirstName;
            employee.LastName = updatedEmployee.LastName;
            employee.Salary = updatedEmployee.Salary;

            employee = await _employeesRepo.UpdateAsync(employee);

            var employeeDto = ApiMapper.Map<Employee, GetEmployeeDto>(employee);
            return employeeDto;
        }

        public async Task<GetEmployeeDto> DeleteAsync(int employeeId)
        {
            var employee = await GetEmployeeAsync(employeeId);
            if (employee == null)
                throw new NoNullAllowedException($"There is no employee with id: {employeeId}");

            employee = await _employeesRepo.DeleteAsync(employee);

            var employeeDto = ApiMapper.Map<Employee, GetEmployeeDto>(employee);
            return employeeDto;
        }

        private async Task<Employee?> GetEmployeeAsync(int employeeId)
        {
            if (employeeId <= 0)
                throw new ArgumentOutOfRangeException(nameof(employeeId), $"employeeId: {employeeId}");

            var employee = await _employeesRepo.GetAsync(employeeId);
            return employee;
        }
    }
}
