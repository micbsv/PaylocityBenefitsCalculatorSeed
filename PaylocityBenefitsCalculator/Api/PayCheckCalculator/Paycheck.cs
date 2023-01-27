using Api.PayCheckCalculator.Deductions;

namespace Api.PayCheckCalculator
{
    public class PayCheck
    {
        public decimal Salary { get; set; }
        public List<IDeduction> Deductions { get; set; }
        public decimal TotalDeductions { get; set; }
    }
}