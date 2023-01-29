using Api.Models;

namespace Api.PaycheckCalculator
{
    public interface IPaycheckCalculator
    {
        Paycheck CalculatePaycheck(Employee employee);
    }
}
