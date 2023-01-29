using Api.Models;

namespace Api.PaycheckCalculator.Deductions
{
    public class PerDependentDeduction : Deduction, IDeduction
    {
        public override string Name => "Dependent benefits";
        public override string Description => "Each dependent represents an additional $600 cost per month (for benefits)";

        public decimal Calculate(Employee employee, int paychecksPerYear)
        {
            var count = employee?.Dependents?.Count ?? 0;
            if (count == 0)
                return 0;

            var costPerMonth = 600m * count;
            Cost = Math.Round(costPerMonth * 12 / paychecksPerYear, 2);
            return Cost;
        }
    }
}
