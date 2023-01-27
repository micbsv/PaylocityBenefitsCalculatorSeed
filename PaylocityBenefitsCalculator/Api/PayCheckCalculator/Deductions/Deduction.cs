namespace Api.PayCheckCalculator.Deductions
{
    public abstract class Deduction
    {
        public decimal Cost { get; set; }

        protected const int paychecksPerYear = 26;
    }
}