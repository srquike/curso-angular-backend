using CursoAngular.BOL;
using CursoAngular.Repository.Stars;
using Microsoft.EntityFrameworkCore;

namespace CursoAngular.DAL.Repositories.Stars
{
    public class StarsRepository : IStarsRepository
    {
        private readonly CursoAngularDbContext _dbContext;

        public StarsRepository(CursoAngularDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<StarEntity>> GetByName(string name)
        {
            return await _dbContext.Stars
                .AsNoTracking()
                .Where(s => s.Name.Contains(name))
                .Take(5)
                .ToListAsync();
        }
    }
}
