using Api.Dtos.Employee;

namespace Api.Validators
{
    public interface IValidator
    {
        (bool isValid, string errorMessage) Validate(AddEmployeeDto employee);
    }
}
