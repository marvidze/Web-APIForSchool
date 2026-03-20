using MongoDB.Driver;
using School.Core.Common;
using School.Core.Errors;
using School.Core.Interfaces;
using School.Core.Models;

namespace School.Persistence.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IMongoCollection<Group> _groupCollection;

        public GroupRepository(MongoDbContext dbContext) 
        { 
            _groupCollection = dbContext.GetCollection<Group>();
        }

        public async Task<Result<Group>> GetByIdAsync(string id)
        {
            var group = await _groupCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (group == null) return Result<Group>.Failure(GroupErrors.NotFound(id));

            return Result<Group>.Success(group);
        }

        public async Task<Result<IEnumerable<Group>>> GetAllAsync()
        {
            var groups = await _groupCollection.Find(_ => true).ToListAsync();

            return Result<IEnumerable<Group>>.Success(groups);
        }

        public async Task<Result<Group>> CreateAsync(Group group)
        {
            await _groupCollection.InsertOneAsync(group);

            return Result<Group>.Success(group);
        }

        public async Task<Result<Group>> UpdateAsync(Group group)
        {
            var isExist = await GetByIdAsync(group.Id);
            if (isExist.IsFailure) return Result<Group>.Failure(GroupErrors.NotFound(group.Id));

            await _groupCollection.ReplaceOneAsync(g => g.Id == group.Id, group);

            return Result<Group>.Success(group);
        }

        public async Task<Result<bool>> DeleteAsync(string id)
        {
            var isExist = await GetByIdAsync(id);
            if (isExist.IsFailure) return Result<bool>.Failure(GroupErrors.NotFound(id));

            await _groupCollection.DeleteOneAsync(g => g.Id == id);

            return Result<bool>.Success(true);
        }

    }
}
