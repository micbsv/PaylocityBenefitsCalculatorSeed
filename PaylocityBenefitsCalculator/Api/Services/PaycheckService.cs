using Api.PaycheckCalculator;
using Api.Repository;
using System.Data;

namespace Api.Services
{
    public class PaycheckService : IPaycheckService
    {
        private readonly IEmployeesRepository _employeesRepo;
        private readonly IPaycheckCalculator _payCheckCalculator;

        public PaycheckService(IEmployeesRepository employeesRepo, IPaycheckCalculator payCheckCalculator)
        {
            _employeesRepo = employeesRepo;
            _payCheckCalculator = payCheckCalculator;
        }

        public async Task<Paycheck> CalculatePaycheckAsync(int employeeId)
        {
            if (employeeId <= 0)
                throw new ArgumentOutOfRangeException(nameof(employeeId), $"employeeId: {employeeId}");

            var employee = await _employeesRepo.GetAsync(employeeId);
            if (employee == null)
                throw new NoNullAllowedException($"There is no employee with id: {employeeId}");

            var payCheck = _payCheckCalculator.CalculatePaycheck(employee);
            return payCheck;
        }
    }
}
