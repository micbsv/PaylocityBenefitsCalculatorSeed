﻿using Api.Models.Enums;
using Api.Models;
using Api.Dtos.Employee;

namespace Api.Validators
{
    public class AddEmployeeValidator : IValidator
    {
        public (bool isValid, string errorMessage) Validate(AddEmployeeDto employee)
        {
            // Validate one-spouse/partnet requirement
            var count = employee.Dependents?
                .Count(d => d.Relationship == Relationship.Spouse || d.Relationship == Relationship.DomesticPartner);

            if (count > 1)
                return (false, "Employee may only have 1 spouse or domestic partner (not both)");

            return (true, string.Empty);
        }
    }
}