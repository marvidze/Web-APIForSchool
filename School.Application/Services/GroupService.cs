using MongoDB.Driver.GridFS;
using School.Application.Contracts.Group;
using School.Application.Interfaces;
using School.Core.Common;
using School.Core.Interfaces;
using School.Core.Models;

namespace School.Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Result<Group>> GetByIdAsync(string id)
        {
            return await _groupRepository.GetByIdAsync(id);
        }

        public async Task<Result<IEnumerable<Group>>> GetAllAsync()
        {
            return await _groupRepository.GetAllAsync();
        }

        public async Task<Result<Group>> CreateAsync(CreateGroupRequest group)
        {
            var newGroup = new Group
            {
                Id = Guid.NewGuid().ToString(),
                Name = group.Name,
            };

            return await _groupRepository.CreateAsync(newGroup);
        }
        
        public async Task<Result<Group>> UpdateAsync(UpdateGroupRequest group)
        {
            var updateGroup = new Group
            { 
                Id = group.Id,
                Name = group.Name,
            };

            return await _groupRepository.UpdateAsync(updateGroup); 
        }

        public async Task<Result<bool>> DeleteAsync(string id)
        {
            return await _groupRepository.DeleteAsync(id);
        }
    }
}
