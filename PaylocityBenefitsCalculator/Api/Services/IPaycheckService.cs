using Api.PaycheckCalculator;

namespace Api.Services
{
    public interface IPaycheckService
    {
        Task<Paycheck> CalculatePaycheckAsync(int employeeId);
    }
}
