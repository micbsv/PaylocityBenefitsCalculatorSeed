using Api.Controllers;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models.Enums;
using Api.PayCheckCalculator;
using Api.Repository;
using Api.Services;
using Api.Validators;
using System;
using System.Linq;
using Xunit;

namespace ApiTests
{
    public class EmployeeControllerTests
    {
        private IEmployeesService _employeesService;
        private EmployeesController _target;

        public EmployeeControllerTests()
        {
            var employeesRepo = new EmployeesRepository();
            var payCheckCalculator = new PayCheckCalculator();
            _employeesService = new EmployeesService(employeesRepo, payCheckCalculator);

            var addEmployeeValidator = new AddEmployeeValidator();
            _target = new EmployeesController(_employeesService, addEmployeeValidator);
        }

        [Fact]
        public async void GetAll()
        {
            var res = await _target.GetAll();
            var data = res.Value?.Data;

            Assert.NotNull(data);
            Assert.True(data?.Count > 0);
        }

        [Fact]
        public async void Get()
        {
            var list = await _employeesService.GetAllAsync();
            var firstEmployee = list.First();
            var id = firstEmployee.Id;

            var res = await _target.Get(id);
            var testEmployee = res.Value?.Data;

            Assert.NotNull(testEmployee);
            Assert.Equal(firstEmployee.Id, testEmployee?.Id);
            Assert.Equal(firstEmployee.FirstName, testEmployee?.FirstName);
            Assert.Equal(firstEmployee.LastName, testEmployee?.LastName);
        }

        [Fact]
        public async void Get_NonExisting()
        {
            var id = 999999999;

            var res = await _target.Get(id);
            var testEmployee = res.Value?.Data;

            Assert.Null(testEmployee);
        }

        [Fact]
        public async void Update()
        {
            var newEmployee = CreateEmployeeDto();
            var addedEmployee = await _employeesService.AddAsync(newEmployee);

            var updatedEmployee = new UpdateEmployeeDto { 
                FirstName = "Sam",
                LastName = "Jhonson",
                Salary = 2000
            };

            var id = addedEmployee.Id;

            var res = await _target.UpdateEmployee(id, updatedEmployee);
            var testEmployee = res.Value?.Data;

            Assert.NotNull(testEmployee);
            Assert.Equal(id, testEmployee?.Id);
            Assert.Equal("Sam", testEmployee?.FirstName);
            Assert.Equal("Jhonson", testEmployee?.LastName);
            Assert.Equal(2000, testEmployee?.Salary);

            // Clean up
            await _employeesService.DeleteAsync(id);
        }

        [Fact]
        public async void Delete()
        {
            var newEmployee = CreateEmployeeDto();
            var addedEmployee = await _employeesService.AddAsync(newEmployee);

            var id = addedEmployee.Id;

            var res = await _target.DeleteEmployee(id);
            var testEmployee = res.Value?.Data;

            Assert.NotNull(testEmployee);
            Assert.Equal(newEmployee.LastName, testEmployee?.LastName);
            Assert.Equal(newEmployee.FirstName, testEmployee?.FirstName);
            Assert.Equal(newEmployee.DateOfBirth, testEmployee?.DateOfBirth);
            Assert.Equal(newEmployee.Salary, testEmployee?.Salary);

            // Make sure there is no this employee in the database anymore
            var ret = await _employeesService.GetAsync(id);
            Assert.Null(ret);
        }

        private AddEmployeeDto CreateEmployeeDto()
        {
            var newEmployee = new AddEmployeeDto
            {
                FirstName = "John",
                LastName = "Smith",
                DateOfBirth = DateTime.Parse("02/09/1972"),
                Salary = 1000,
                Dependents = new[] {
                    new AddDependentDto {
                        FirstName = "Tom",
                        LastName = "Smith",
                        DateOfBirth = DateTime.Parse("03/10/2003"),
                        Relationship = Relationship.Child
                    }
                }
            };
            return newEmployee;
        }

        [Fact]
        public async void Add()
        {
            var newEmployee = CreateEmployeeDto();

            var res = await _target.AddEmployee(newEmployee);
            var testEmployee = res.Value?.Data;

            Assert.NotNull(testEmployee);

            var id = testEmployee?.Id ?? 0;
            Assert.True(id > 0);
            Assert.Equal(newEmployee.LastName, testEmployee?.LastName);
            Assert.Equal(newEmployee.FirstName, testEmployee?.FirstName);
            Assert.Equal(newEmployee.DateOfBirth, testEmployee?.DateOfBirth);
            Assert.Equal(newEmployee.Salary, testEmployee?.Salary);

            // Check dependent
            Assert.NotNull(testEmployee?.Dependents);
            Assert.Equal(1, testEmployee?.Dependents?.Count);

            var newDependent = newEmployee?.Dependents?.First();
            var testDependent = testEmployee?.Dependents?.First();

            Assert.Equal(newDependent.LastName, testDependent?.LastName);
            Assert.Equal(newDependent.FirstName, testDependent?.FirstName);
            Assert.Equal(newDependent.DateOfBirth, testDependent?.DateOfBirth);
            Assert.Equal(newDependent.Relationship, testDependent?.Relationship);

            // Clean up
            await _employeesService.DeleteAsync(id);
        }


        [Fact]
        public async void Add_TwoSpouses_InvalidCase()
        {
            var newEmployee = CreateEmployeeDto();
            newEmployee.Dependents = new[] {
                new AddDependentDto {
                    FirstName = "Sarah",
                    LastName = "Smith",
                    DateOfBirth = DateTime.Parse("03/10/1973"),
                    Relationship = Relationship.Spouse
                },
                new AddDependentDto
                {
                    FirstName = "Emma",
                    LastName = "Brown",
                    DateOfBirth = DateTime.Parse("04/11/1974"),
                    Relationship = Relationship.Spouse
                }
            };

            var res = await _target.AddEmployee(newEmployee);

            Assert.False(res.Value?.Success);
            Assert.False(string.IsNullOrEmpty(res.Value?.Error));
            Assert.Null(res.Value?.Data);
        }
    }
}
