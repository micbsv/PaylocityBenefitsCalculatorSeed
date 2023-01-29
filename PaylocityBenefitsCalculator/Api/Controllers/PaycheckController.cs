using Api.Models;
using Api.PaycheckCalculator;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PaycheckController : ControllerBase
    {
        private readonly IPaycheckService _paycheckService;

        public PaycheckController(IPaycheckService paycheckService)
        {
            _paycheckService = paycheckService;
        }

        [SwaggerOperation(Summary = "Calculate paycheck for employee id. Returns paycheck object")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Paycheck>>> Calculate(int id)
        {
            Paycheck paycheck = await _paycheckService.CalculatePaycheckAsync(id);

            var result = new ApiResponse<Paycheck> {
                Data = paycheck,
                Success = true
            };
            return result;
        }
    }
}
