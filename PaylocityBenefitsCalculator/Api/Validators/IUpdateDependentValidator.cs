using Api.Dtos.Dependent;

namespace Api.Validators
{
    public interface IUpdateDependentValidator
    {
        Task<(bool isValid, string errorMessage)> ValidateAsync(int id, UpdateDependentDto dependent);
    }
}