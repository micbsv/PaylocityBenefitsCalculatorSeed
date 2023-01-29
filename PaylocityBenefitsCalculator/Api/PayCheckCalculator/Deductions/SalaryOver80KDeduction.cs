using Api.Models;

namespace Api.PaycheckCalculator.Deductions
{
    public class SalaryOver80KDeduction : Deduction, IDeduction
    {
        public override string Name => "Salary over $80,000 per year";
        public override string Description => "Employees that make more than $80,000 per year will incur an additional 2% of their yearly salary in benefits costs";

        public decimal Calculate(Employee employee, int paychecksPerYear)
        {
            if (employee.Salary <= 80000)
                return 0;

            Cost = Math.Round(employee.Salary * 0.02m / paychecksPerYear, 2);
            return Cost;
        }
    }
}
