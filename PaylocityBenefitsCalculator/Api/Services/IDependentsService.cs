using Api.Dtos.Dependent;

namespace Api.Services
{
    public interface IDependentsService
    {
        Task<List<GetDependentDto>> GetAllAsync();
        Task<GetDependentDto?> GetAsync(int dependentId);
        Task<GetDependentDto> AddAsync(AddDependentDto Dependent);
        Task<GetDependentDto> UpdateAsync(int dependentId, UpdateDependentDto updatedDependent);
        Task<GetDependentDto> DeleteAsync(int dependentId);
    }
}
