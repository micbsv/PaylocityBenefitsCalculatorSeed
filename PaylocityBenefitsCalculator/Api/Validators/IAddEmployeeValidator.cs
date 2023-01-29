using Api.Dtos.Employee;

namespace Api.Validators
{
    public interface IAddEmployeeValidator
    {
        (bool isValid, string errorMessage) Validate(AddEmployeeDto employee);
    }
}
