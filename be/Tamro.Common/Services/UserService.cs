using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Tamro.Common.Interfaces;
using Tamro.Entities;

namespace Tamro.Common.Services
{
    public class UserService : IUserService
    {
        private readonly TamroDBContext _dbContext;

        public UserService(TamroDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(UserEntity entity)
        {
            _dbContext.Users.Add(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task Update(UserEntity entity)
        {
            _dbContext.Users.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserEntity>> Load()
        {
            var result = await _dbContext.Users.ToListAsync();
            return result;
        }

        public async Task<UserEntity> Get(int id)
        {
            var result = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<UserEntity> GetByEmail(string email)
        {
            var result = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
            return result;
        }

        public async Task Delete(UserEntity entity)
        {
            _dbContext.Users.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
