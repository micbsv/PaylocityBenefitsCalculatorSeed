using Api.Dtos.Dependent;
using Api.Mappers;
using Api.Models;
using Api.Repository;
using System.Data;

namespace Api.Services
{
    // This class provides abstraction from Repository and handles mapping db entities to dtos
    public class DependentsService : IDependentsService
    {
        private readonly IDependentsRepository _dependentsRepo;

        public DependentsService(IDependentsRepository dependentsRepo)
        {
            _dependentsRepo = dependentsRepo;
        }

        public async Task<List<GetDependentDto>> GetAllAsync()
        {
            var dependents = await _dependentsRepo.GetAllAsync();
            var dependentDtos = ApiMapper.Map<Dependent, GetDependentDto>(dependents.AsQueryable());

            var list = dependentDtos.ToList();
            return list;
        }

        public async Task<GetDependentDto?> GetAsync(int dependentId)
        {
            var dependent = await GetDependentAsync(dependentId);
            if (dependent == null)
                return null;

            var dependentDto = ApiMapper.Map<Dependent, GetDependentDto>(dependent);
            return dependentDto;
        }

        public async Task<GetDependentDto> AddAsync(AddDependentDto dependent)
        {
            if (dependent == null)
                throw new ArgumentNullException(nameof(dependent));

            var dependentModel = ApiMapper.Map<AddDependentDto, Dependent>(dependent);
            dependentModel = await _dependentsRepo.AddAsync(dependentModel);
            
            var dependentDto = ApiMapper.Map<Dependent, GetDependentDto>(dependentModel);
            return dependentDto;
        }

        public async Task<GetDependentDto> UpdateAsync(int dependentId, UpdateDependentDto updatedDependent)
        {
            var dependent = await GetDependentAsync(dependentId);
            if (dependent == null)
                throw new NoNullAllowedException($"There is no dependent with id: {dependentId}");

            dependent.FirstName = updatedDependent.FirstName;
            dependent.LastName = updatedDependent.LastName;
            dependent.DateOfBirth = updatedDependent.DateOfBirth;
            dependent.Relationship = updatedDependent.Relationship;

            dependent = await _dependentsRepo.UpdateAsync(dependent);

            var dependentDto = ApiMapper.Map<Dependent, GetDependentDto>(dependent);
            return dependentDto;
        }

        public async Task<GetDependentDto> DeleteAsync(int dependentId)
        {
            var dependent = await GetDependentAsync(dependentId);
            if (dependent == null)
                throw new NoNullAllowedException($"There is no dependent with id: {dependentId}");

            dependent = await _dependentsRepo.DeleteAsync(dependent);

            var dependentDto = ApiMapper.Map<Dependent, GetDependentDto>(dependent);
            return dependentDto;
        }

        private async Task<Dependent?> GetDependentAsync(int dependentId)
        {
            if (dependentId <= 0)
                throw new ArgumentOutOfRangeException(nameof(dependentId), $"dependentId: {dependentId}");

            var dependent = await _dependentsRepo.GetAsync(dependentId);
            return dependent;
        }
    }
}
