using Api.Dtos.Dependent;
using Api.Models.Enums;
using Api.Repository;
using System.Data;

namespace Api.Validators
{
    public class AddDependentValidator : IAddDependentValidator
    {
        private readonly IEmployeesRepository _employeesRepo;

        public AddDependentValidator(IEmployeesRepository employeesRepo)
        {
            _employeesRepo = employeesRepo;
        }

        public async Task<(bool isValid, string errorMessage)> ValidateAsync(AddDependentWithEmployeeIdDto addingDependent)
        {
            if (addingDependent.Relationship != Relationship.Spouse &&
                addingDependent.Relationship != Relationship.DomesticPartner)
            {
                return (true, string.Empty);
            }

            var employee = await _employeesRepo.GetAsync(addingDependent.EmployeeId);
            if (employee == null)
                throw new NoNullAllowedException($"There is no employee with id: {addingDependent.EmployeeId}");

            var count = employee.Dependents?
                .Count(d => d.Relationship == Relationship.Spouse || d.Relationship == Relationship.DomesticPartner);

            if (count > 0)
                return (false, "Employee may only have 1 spouse or domestic partner (not both)");

            return (true, string.Empty);
        }
    }
}
