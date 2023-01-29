using Api.Models;

namespace Api.PaycheckCalculator.Deductions
{
    public class BasicBenefitsDeduction : Deduction, IDeduction
    {
        public override string Name => "Basic benefits";
        public override string Description => "Employees have a base cost of $1,000 per month (for benefits)";

        public decimal Calculate(Employee employee, int paychecksPerYear)
        {
            var costPerMonth = 1000m;
            Cost = Math.Round(costPerMonth * 12 / paychecksPerYear, 2);

            return Cost;
        }
    }
}
