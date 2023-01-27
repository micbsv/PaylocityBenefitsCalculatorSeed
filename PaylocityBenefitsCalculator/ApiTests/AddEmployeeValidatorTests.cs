using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models.Enums;
using Api.Validators;
using System;
using Xunit;

namespace ApiTests
{
    public class AddEmployeeValidatorTests
    {
        IValidator _target;

        public AddEmployeeValidatorTests()
        {
            _target = new AddEmployeeValidator();
        }

        [Fact]
        public void Add_TwoChildren()
        {
            var newEmployee = new AddEmployeeDto();
            newEmployee.Dependents = new[] {
                new AddDependentDto {
                    FirstName = "Sarah",
                    LastName = "Smith",
                    DateOfBirth = DateTime.Parse("03/10/2000"),
                    Relationship = Relationship.Child
                },
                new AddDependentDto {
                    FirstName = "Emma",
                    LastName = "Brown",
                    DateOfBirth = DateTime.Parse("04/11/2001"),
                    Relationship = Relationship.Child
                }
            };

            var (isValid, errorMessage) = _target.Validate(newEmployee);

            Assert.True(isValid);
            Assert.True(string.IsNullOrEmpty(errorMessage));
        }

        [Fact]
        public void Add_TwoChildrenAndSpouse()
        {
            var newEmployee = new AddEmployeeDto();
            newEmployee.Dependents = new[] {
                new AddDependentDto {
                    FirstName = "Mary",
                    LastName = "Miller",
                    DateOfBirth = DateTime.Parse("03/10/2000"),
                    Relationship = Relationship.Spouse
                },
                new AddDependentDto {
                    FirstName = "Sarah",
                    LastName = "Smith",
                    DateOfBirth = DateTime.Parse("03/10/2000"),
                    Relationship = Relationship.Child
                },
                new AddDependentDto {
                    FirstName = "Emma",
                    LastName = "Brown",
                    DateOfBirth = DateTime.Parse("04/11/2001"),
                    Relationship = Relationship.Child
                }
            };

            var (isValid, errorMessage) = _target.Validate(newEmployee);

            Assert.True(isValid);
            Assert.True(string.IsNullOrEmpty(errorMessage));
        }

        [Fact]
        public void Add_TwoChildrenAndPartner()
        {
            var newEmployee = new AddEmployeeDto();
            newEmployee.Dependents = new[] {
                new AddDependentDto {
                    FirstName = "James",
                    LastName = "Miller",
                    DateOfBirth = DateTime.Parse("03/10/2000"),
                    Relationship = Relationship.DomesticPartner
                },
                new AddDependentDto {
                    FirstName = "Sarah",
                    LastName = "Smith",
                    DateOfBirth = DateTime.Parse("03/10/2000"),
                    Relationship = Relationship.Child
                },
                new AddDependentDto {
                    FirstName = "Emma",
                    LastName = "Brown",
                    DateOfBirth = DateTime.Parse("04/11/2001"),
                    Relationship = Relationship.Child
                }
            };

            var (isValid, errorMessage) = _target.Validate(newEmployee);

            Assert.True(isValid);
            Assert.True(string.IsNullOrEmpty(errorMessage));
        }

        [Fact]
        public void Add_TwoSpouses()
        {
            var newEmployee = new AddEmployeeDto();
            newEmployee.Dependents = new[] {
                new AddDependentDto {
                    FirstName = "Sarah",
                    LastName = "Smith",
                    DateOfBirth = DateTime.Parse("03/10/1973"),
                    Relationship = Relationship.Spouse
                },
                new AddDependentDto {
                    FirstName = "Emma",
                    LastName = "Brown",
                    DateOfBirth = DateTime.Parse("04/11/1974"),
                    Relationship = Relationship.Spouse
                }
            };

            var (isValid, errorMessage) = _target.Validate(newEmployee);

            Assert.False(isValid);
            Assert.False(string.IsNullOrEmpty(errorMessage));
        }

        [Fact]
        public void Add_TwoPartners()
        {
            var newEmployee = new AddEmployeeDto();
            newEmployee.Dependents = new[] {
                new AddDependentDto {
                    FirstName = "Sarah",
                    LastName = "Smith",
                    DateOfBirth = DateTime.Parse("03/10/1973"),
                    Relationship = Relationship.DomesticPartner
                },
                new AddDependentDto {
                    FirstName = "Emma",
                    LastName = "Brown",
                    DateOfBirth = DateTime.Parse("04/11/1974"),
                    Relationship = Relationship.DomesticPartner
                }
            };

            var (isValid, errorMessage) = _target.Validate(newEmployee);

            Assert.False(isValid);
            Assert.False(string.IsNullOrEmpty(errorMessage));
        }

        [Fact]
        public void Add_SpouseAndPartner()
        {
            var newEmployee = new AddEmployeeDto();
            newEmployee.Dependents = new[] {
                new AddDependentDto {
                    FirstName = "Sarah",
                    LastName = "Smith",
                    DateOfBirth = DateTime.Parse("03/10/1973"),
                    Relationship = Relationship.Spouse
                },
                new AddDependentDto {
                    FirstName = "Sam",
                    LastName = "Brown",
                    DateOfBirth = DateTime.Parse("04/11/1974"),
                    Relationship = Relationship.DomesticPartner
                }
            };

            var (isValid, errorMessage) = _target.Validate(newEmployee);

            Assert.False(isValid);
            Assert.False(string.IsNullOrEmpty(errorMessage));
        }
    }
}
