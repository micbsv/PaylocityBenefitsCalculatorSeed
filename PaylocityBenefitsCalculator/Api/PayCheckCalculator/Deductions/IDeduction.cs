using Api.Models;

namespace Api.PayCheckCalculator.Deductions
{
    public interface IDeduction
    {
        public string Name { get; }
        public string Description { get; }
        decimal Calculate(Employee employee);
    }
}
