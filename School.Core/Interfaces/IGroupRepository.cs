using School.Core.Common;
using School.Core.Models;

namespace School.Core.Interfaces
{
    public interface IGroupRepository
    {
        Task<Result<Group>> GetByIdAsync(string id);

        Task<Result<IEnumerable<Group>>> GetAllAsync();

        Task<Result<Group>> CreateAsync(Group group);

        Task<Result<Group>> UpdateAsync(Group group);

        Task<Result<bool>> DeleteAsync(string id);
    }
}
