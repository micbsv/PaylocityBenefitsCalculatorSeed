using Api.Models;

namespace Api.PaycheckCalculator.Deductions
{
    public class PerDependentOver50Deduction : Deduction, IDeduction
    {
        public override string Name => "Dependents over 50 years old";
        public override string Description => "Dependents that are over 50 years old will incur an additional $200 per month";

        public decimal Calculate(Employee employee, int paychecksPerYear)
        {
            var dateLimit = DateTime.Today.AddYears(-50);

            var count = employee?.Dependents?.Count(d => d.DateOfBirth <= dateLimit) ?? 0;
            if (count == 0)
                return 0;

            var costPerMonth = 200m * count;
            Cost = Math.Round(costPerMonth * 12 / paychecksPerYear, 2);
            return Cost;
        }
    }
}
