using Api.Models;
using Api.PayCheckCalculator.Deductions;

namespace Api.PayCheckCalculator
{
    public class PayCheckCalculator : IPayCheckCalculator
    {
        public PayCheck CalculateDeductions(Employee employee)
        {
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

            return new PayCheck {
                Salary = employee.Salary,
                Deductions = deductions,
                TotalDeductions = total
            };
        }
    }
}
