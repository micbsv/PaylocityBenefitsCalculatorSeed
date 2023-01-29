using Api.Dtos.Dependent;
using Api.Models.Enums;
using Api.Repository;
using System.Data;

namespace Api.Validators
{
    public class UpdateDependentValidator : IUpdateDependentValidator
    {
        private readonly IDependentsRepository _dependentsRepo;
        private readonly IEmployeesRepository _employeesRepo;
        
        public UpdateDependentValidator(IDependentsRepository dependentsRepo, IEmployeesRepository employeesRepo)
        {
            _dependentsRepo = dependentsRepo;
            _employeesRepo = employeesRepo;
        }

        public async Task<(bool isValid, string errorMessage)> ValidateAsync(int dependentId, UpdateDependentDto updatingDependent)
        {
            if (updatingDependent.Relationship != Relationship.Spouse &&
                updatingDependent.Relationship != Relationship.DomesticPartner)
            {
                return (true, string.Empty);
            }

            var dependent = await _dependentsRepo.GetAsync(dependentId);
            if (dependent == null)
                throw new NoNullAllowedException($"There is no dependent with id: {dependentId}");

            var employee = await _employeesRepo.GetAsync(dependent.EmployeeId);
            if (employee == null)
                throw new NoNullAllowedException($"There is no employee with id: {dependent.EmployeeId}");

            var checkDependent = employee.Dependents?
                .FirstOrDefault(d => d.Relationship == Relationship.Spouse || d.Relationship == Relationship.DomesticPartner);

            if (checkDependent != null && checkDependent.Id != dependentId)
                return (false, "Employee may only have 1 spouse or domestic partner (not both)");

            return (true, string.Empty);
        }
    }
}
