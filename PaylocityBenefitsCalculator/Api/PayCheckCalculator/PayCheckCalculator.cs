using Api.Models;
using Api.PaycheckCalculator.Deductions;

namespace Api.PaycheckCalculator
{
    public class PaycheckCalculator : IPaycheckCalculator
    {
        public static int PaychecksPerYear = 26;

        public Paycheck CalculatePaycheck(Employee employee)
        {
            // Use Command Pattern
            var deductions = new List<IDeduction> {
                new BasicBenefitsDeduction(),
                new PerDependentDeduction(),
                new SalaryOver80KDeduction(),
                new PerDependentOver50Deduction()
            };

            var totalDeductions = 0m;
            foreach (var deduction in deductions)
            {
                totalDeductions += deduction.Calculate(employee, PaychecksPerYear);
            }

            var salary = Math.Round(employee.Salary / PaychecksPerYear, 2);
            var netPaycheck = Math.Round(salary - totalDeductions, 2);

            return new Paycheck {
                Gross = salary,
                Deductions = deductions.Cast<Deduction>().ToList(),
                TotalDeductions = totalDeductions,
                NetPay = netPaycheck
            };
        }
    }
}
