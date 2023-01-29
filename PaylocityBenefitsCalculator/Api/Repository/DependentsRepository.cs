using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    // This class provides communication with the databse and outputs database entities
    public class DependentsRepository : IDependentsRepository
    {
        public async Task<List<Dependent>> GetAllAsync()
        {
            using var context = new ApiDbContext();
            var list = await context.Dependents
                .Include(e => e.Employee)
                .ToListAsync();

            return list;
        }

        public async Task<Dependent?> GetAsync(int dependentId)
        {
            using var context = new ApiDbContext();
            var dependent = await context.Dependents
                .Include(e => e.Employee)
                .SingleOrDefaultAsync(e => e.Id == dependentId);

            return dependent;
        }

        public async Task<Dependent> AddAsync(Dependent dependent)
        {
            using var context = new ApiDbContext();
            var dependentEntity = await context.Dependents.AddAsync(dependent);
            await context.SaveChangesAsync();

            return dependentEntity.Entity;
        }

        public async Task<Dependent> UpdateAsync(Dependent dependent)
        {
            using var context = new ApiDbContext();
            var dependentEntity = context.Dependents.Update(dependent);
            await context.SaveChangesAsync();

            return dependentEntity.Entity;
        }

        public async Task<Dependent> DeleteAsync(Dependent dependent)
        {
            using var context = new ApiDbContext();
            var dependentEntity = context.Dependents.Remove(dependent);
            await context.SaveChangesAsync();

            return dependentEntity.Entity;
        }
    }
}
