using School.Application.Contracts.Group;
using School.Core.Common;
using School.Core.Models;

namespace School.Application.Interfaces
{
    public interface IGroupService
    {
        Task<Result<Group>> GetByIdAsync(string id);

        Task<Result<IEnumerable<Group>>> GetAllAsync();

        Task<Result<Group>> CreateAsync(CreateGroupRequest group);

        Task<Result<Group>> UpdateAsync(UpdateGroupRequest group);

        Task<Result<bool>> DeleteAsync(string id);
    }
}
