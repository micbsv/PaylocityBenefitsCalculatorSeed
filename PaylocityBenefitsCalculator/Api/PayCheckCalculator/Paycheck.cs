using Api.PaycheckCalculator.Deductions;

namespace Api.PaycheckCalculator
{
    public class Paycheck
    {
        public decimal Gross { get; set; }
        public List<Deduction> Deductions { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetPay { get; set; }

        public Paycheck()
        {
            Deductions = new List<Deduction>();
        }
    }
}