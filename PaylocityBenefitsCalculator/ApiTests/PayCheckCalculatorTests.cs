using Api.Models;
using Api.PaycheckCalculator;
using Api.PaycheckCalculator.Deductions;
using System;
using Xunit;

namespace ApiTests
{
    public class PaycheckCalculatorTests
    {
        private int _paychecksPerYear = PaycheckCalculator.PaychecksPerYear;

        [Fact]
        public void BasicBenefits()
        {
            var employee = new Employee {
                Salary = 50000m
            };

            var deduction = new BasicBenefitsDeduction();
            var cost = deduction.Calculate(employee, _paychecksPerYear);

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
            var cost = deduction.Calculate(employee, _paychecksPerYear);

            Assert.Equal(553.85m, cost);
        }

        [Fact]
        public void SalaryOver80K()
        {
            var employee = new Employee {
                Salary = 120000m
            };

            var deduction = new SalaryOver80KDeduction();
            var cost = deduction.Calculate(employee, _paychecksPerYear);

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
                        // Test case for today birth day
                        DateOfBirth = DateTime.Today.AddYears(-50)
                    }
                }
            };

            var deduction = new PerDependentOver50Deduction();
            var cost = deduction.Calculate(employee, _paychecksPerYear);

            Assert.Equal(184.62m, cost);
        }

        [Fact]
        public void CalculateDeductions()
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
                        // Test case for today birth day
                        DateOfBirth = DateTime.Today.AddYears(-50)
                    }
                }
            };

            var _target = new PaycheckCalculator();
            var paycheck = _target.CalculatePaycheck(employee);

            Assert.Equal(4615.38m, paycheck.Gross);
            Assert.Equal(1569.24m, paycheck.TotalDeductions);
            Assert.Equal(3046.14m, paycheck.NetPay);
        }
    }
}
