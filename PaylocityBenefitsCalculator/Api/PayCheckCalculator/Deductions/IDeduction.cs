using Api.Models;

namespace Api.PaycheckCalculator.Deductions
{
    public interface IDeduction
    {
        decimal Calculate(Employee employee, int paychecksPerYear);
    }
}
