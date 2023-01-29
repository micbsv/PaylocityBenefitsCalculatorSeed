using Api.Dtos.Dependent;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DependentsController : ControllerBase
    {
        private readonly IDependentsService _dependentsService;

        public DependentsController(IDependentsService dependentsService)
        {
            _dependentsService = dependentsService;
        }

        [SwaggerOperation(Summary = "Get dependent by id")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
        {
            var dependent = await _dependentsService.GetAsync(id);

            var result = new ApiResponse<GetDependentDto> {
                Data = dependent,
                Success = true
            };
            return result;
        }

        [SwaggerOperation(Summary = "Get all dependents")]
        [HttpGet("")]
        public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
        {
            var dependents = await _dependentsService.GetAllAsync();

            var result = new ApiResponse<List<GetDependentDto>> {
                Data = dependents,
                Success = true
            };
            return result;
        }

        [SwaggerOperation(Summary = "Add dependent")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<GetDependentDto>>> AddDependent(AddDependentWithEmployeeIdDto newDependent)
        {
            var dependent = await _dependentsService.AddAsync(newDependent);

            var result = new ApiResponse<GetDependentDto> {
                Data = dependent,
                Success = true
            };
            return result;
        }

        [SwaggerOperation(Summary = "Update dependent")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<GetDependentDto>>> UpdateDependent(int id, UpdateDependentDto updatedDependent)
        {
            GetDependentDto dependent = await _dependentsService.UpdateAsync(id, updatedDependent);

            var result = new ApiResponse<GetDependentDto> {
                Data = dependent,
                Success = true
            };
            return result;
        }

        [SwaggerOperation(Summary = "Delete dependent")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<GetDependentDto>>> DeleteDependent(int id)
        {
            GetDependentDto dependent = await _dependentsService.DeleteAsync(id);

            var result = new ApiResponse<GetDependentDto> {
                Data = dependent,
                Success = true
            };
            return result;
        }
    }
}
