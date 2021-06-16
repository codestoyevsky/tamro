using System.Collections.Generic;
using System.Threading.Tasks;
using Tamro.Entities;

namespace Tamro.Common.Interfaces
{
    public interface IUserService
    {
        Task<int> Create(UserEntity entity);
        Task Update(UserEntity entity);
        Task<IEnumerable<UserEntity>> Load();
        Task<UserEntity> Get(int id);
        Task<UserEntity> GetByEmail(string email);
        Task Delete(UserEntity entity);
    }
}
