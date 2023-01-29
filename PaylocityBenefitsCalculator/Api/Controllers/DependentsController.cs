using Api.Dtos.Dependent;
using Api.Models;
using Api.Services;
using Api.Validators;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DependentsController : ControllerBase
    {
        private readonly IDependentsService _dependentsService;
        private readonly IAddDependentValidator _addDependentValidator;
        private readonly IUpdateDependentValidator _updateDependentValidator;

        public DependentsController(IDependentsService dependentsService, IAddDependentValidator addDependentValidator, 
            IUpdateDependentValidator updateDependentValidator)
        {
            _dependentsService = dependentsService;
            _addDependentValidator = addDependentValidator;
            _updateDependentValidator = updateDependentValidator;
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
            var ret = await _addDependentValidator.ValidateAsync(newDependent);
            if (!ret.isValid)
            {
                var errorResult = new ApiResponse<GetDependentDto>
                {
                    Data = null,
                    Success = false,
                    Error = ret.errorMessage
                };
                return errorResult;
            }

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
            var ret = await _updateDependentValidator.ValidateAsync(id, updatedDependent);
            if (!ret.isValid)
            {
                var errorResult = new ApiResponse<GetDependentDto>
                {
                    Data = null,
                    Success = false,
                    Error = ret.errorMessage
                };
                return errorResult;
            }

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
