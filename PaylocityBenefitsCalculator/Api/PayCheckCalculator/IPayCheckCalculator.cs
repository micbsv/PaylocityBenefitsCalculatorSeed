using Api.Models;
using Api.PayCheckCalculator.Deductions;

namespace Api.PayCheckCalculator
{
    public interface IPayCheckCalculator
    {
        PayCheck CalculateDeductions(Employee employee);
    }
}
