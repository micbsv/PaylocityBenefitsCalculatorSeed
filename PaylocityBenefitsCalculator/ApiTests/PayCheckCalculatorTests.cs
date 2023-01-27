using Api.Models;
using Api.PayCheckCalculator.Deductions;
using System;
using System.Collections.Generic;
using Xunit;

namespace ApiTests
{
    public class PayCheckCalculatorTests
    {
        [Fact]
        public void BasicBenefits()
        {
            var employee = new Employee {
                Salary = 50000m
            };

            var deduction = new BasicBenefitsDeduction();
            var cost = deduction.Calculate(employee);

            Assert.Equal(461.54m, cost);
        }

        [Fact]
        public void PerDependent()
        {
            var employee = new Employee {
                Dependents = new[] {
                    new Dependent(),
                    new Dependent()
                }
            };

            var deduction = new PerDependentDeduction();
            var cost = deduction.Calculate(employee);

            Assert.Equal(553.85m, cost);
        }

        [Fact]
        public void SalaryOver80K()
        {
            var employee = new Employee {
                Salary = 120000m
            };

            var deduction = new SalaryOver80KDeduction();
            var cost = deduction.Calculate(employee);

            Assert.Equal(92.31m, cost);
        }

        [Fact]
        public void PerDependentOver50()
        {
            var employee = new Employee {
                Dependents = new[] {
                    new Dependent {
                        DateOfBirth = DateTime.Parse("01/01/2020")
                    },
                    new Dependent {
                        DateOfBirth = DateTime.Parse("01/01/1970")
                    },
                    new Dependent {
                        DateOfBirth = DateTime.Today.AddYears(-50)
                    }
                }
            };

            var deduction = new PerDependentOver50Deduction();
            var cost = deduction.Calculate(employee);

            Assert.Equal(184.62m, cost);
        }

        [Fact]
        public void CalculatePerPayCheck()
        {
            var employee = new Employee {
                Salary = 120000m,
                Dependents = new[] {
                    new Dependent {
                        DateOfBirth = DateTime.Parse("01/01/2020")
                    },
                    new Dependent {
                        DateOfBirth = DateTime.Parse("01/01/1970")
                    },
                    new Dependent {
                        DateOfBirth = DateTime.Today.AddYears(-50)
                    }
                }
            };

            var deductions = new List<IDeduction> {
                new BasicBenefitsDeduction(),
                new PerDependentDeduction(),
                new SalaryOver80KDeduction(),
                new PerDependentOver50Deduction()
            };

            var total = 0m;
            foreach (var deduction in deductions)
            {
                total += deduction.Calculate(employee);
            }

            Assert.Equal(1569.24m, total);
        }
    }
}
