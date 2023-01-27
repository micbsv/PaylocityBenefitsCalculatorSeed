using Api.Dtos.Employee;
using Api.Models;
using Api.PayCheckCalculator;
using Api.Services;
using Api.Validators;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly IValidator _addEmployeeValidator;

        public EmployeesController(IEmployeesService employeesService, IValidator addEmployeeValidator)
        {
            _employeesService = employeesService;
            _addEmployeeValidator = addEmployeeValidator;
        }

        [SwaggerOperation(Summary = "Get employee by id. Returns employee for given id")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
        {
            var employee = await _employeesService.GetAsync(id);

            var result = new ApiResponse<GetEmployeeDto> {
                Data = employee,
                Success = true
            };
            return result;
        }

        [SwaggerOperation(Summary = "Get all employees. Returns the list of employees in the database")]
        [HttpGet("")]
        public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
        {
            var employees = await _employeesService.GetAllAsync();
                    
            var result = new ApiResponse<List<GetEmployeeDto>> {
                Data = employees,
                Success = true
            };
            return result;
        }

        [SwaggerOperation(Summary = "Add employee. Returns added employee")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> AddEmployee(AddEmployeeDto newEmployee)
        {
            var ret = _addEmployeeValidator.Validate(newEmployee);
            if (!ret.isValid)
            {
                var errorResult = new ApiResponse<GetEmployeeDto> {
                    Data = null,
                    Success = false,
                    Error = ret.errorMessage
                };
                return errorResult;
            }

            var employee = await _employeesService.AddAsync(newEmployee);

            var result = new ApiResponse<GetEmployeeDto> {
                Data = employee,
                Success = true
            };
            return result;
        }

        [SwaggerOperation(Summary = "Update employee. Returns updated employee")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> UpdateEmployee(int id, UpdateEmployeeDto updatedEmployee)
        {
            GetEmployeeDto employee = await _employeesService.UpdateAsync(id, updatedEmployee);

            var result = new ApiResponse<GetEmployeeDto> {
                Data = employee,
                Success = true
            };
            return result;
        }

        [SwaggerOperation(Summary = "Delete employee. Returns previous state of the deleted employee")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> DeleteEmployee(int id)
        {
            GetEmployeeDto employee = await _employeesService.DeleteAsync(id);

            var result = new ApiResponse<GetEmployeeDto> {
                Data = employee,
                Success = true
            };
            return result;
        }

        [SwaggerOperation(Summary = "Calculate paycheck for employee id. Returns paycheck object")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<PayCheck>>> CalculatePayCheck(int id)
        {
            PayCheck paycheck = await _employeesService.CalculatePayCheckAsync(id);

            var result = new ApiResponse<PayCheck> {
                Data = paycheck,
                Success = true
            };
            return result;
        }
    }
}
