using Api.Models;

namespace Api.Repository
{
    public interface IDependentsRepository
    {
        Task<Dependent> AddAsync(Dependent employee);
        Task<List<Dependent>> GetAllAsync();
        Task<Dependent?> GetAsync(int employeeId);
        Task<Dependent> UpdateAsync(Dependent employee);
        Task<Dependent> DeleteAsync(Dependent employee);
    }
}
