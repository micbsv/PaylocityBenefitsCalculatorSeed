using Api.Dtos.Dependent;

namespace Api.Validators
{
    public interface IAddDependentValidator
    {
        Task<(bool isValid, string errorMessage)> ValidateAsync(AddDependentWithEmployeeIdDto addingDependent);
    }
}
