using Api.Models;

namespace Api.PayCheckCalculator.Deductions
{
    public class BasicBenefitsDeduction : Deduction, IDeduction
    {
        public string Name => "Benefits cost";
        public string Description => "Employees have a base cost of $1,000 per month (for benefits)";

        public decimal Calculate(Employee employee)
        {
            var costPerMonth = 1000m;
            Cost = Math.Round(costPerMonth * 12 / paychecksPerYear, 2);

            return Cost;
        }
    }
}
