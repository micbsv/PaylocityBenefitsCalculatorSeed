namespace Api.PaycheckCalculator.Deductions
{
    public abstract class Deduction
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public decimal Cost { get; set; }
    }
}