using MongoDB.Driver;
using School.Core.Common;
using School.Core.Interfaces;
using School.Core.Models;
using School.Core.Errors;

namespace School.Persistence.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly IMongoCollection<Teacher> _teachers;

        public TeacherRepository(MongoDbContext dbContext)
        {
            _teachers = dbContext.GetCollection<Teacher>();
        }

        public async Task<Result<Teacher>> CreateAsync(Teacher teacher)
        {
            var existingEmail = await _teachers.Find(t => t.Email == teacher.Email).FirstOrDefaultAsync();
            if (existingEmail != null)
                return Result<Teacher>.Failure(TeacherErrors.ConflictEmail());

            await _teachers.InsertOneAsync(teacher);

            return Result<Teacher>.Success(teacher);
            
        }

        public async Task<Result<bool>> DeleteAsync(string id)
        {
            var isExist = await GetByIdAsync(id);
            if (isExist.IsFailure) return Result<bool>.Failure(TeacherErrors.NotFound(id));
            
            await _teachers.DeleteOneAsync(t => t.Id == id);

            return Result<bool>.Success(true);
        }

        public async Task<Result<IEnumerable<Teacher>>> GetAllAsync()
        {
            var teachers = await _teachers.Find(_ => true).ToListAsync();

            return Result<IEnumerable<Teacher>>.Success(teachers);
        }

        public async Task<Result<Teacher>> GetByIdAsync(string id)
        {
            var teacher = await _teachers.Find(t => t.Id == id).FirstOrDefaultAsync();
            if (teacher == null) return Result<Teacher>.Failure(TeacherErrors.NotFound(id));

            return Result<Teacher>.Success(teacher);
        }

        public async Task<Result<Teacher>> UpdateAsync(Teacher teacher)
        {
            var isExist = await GetByIdAsync(teacher.Id);
            if (isExist.IsFailure) return Result<Teacher>.Failure(TeacherErrors.NotFound(teacher.Id));

            var existingEmail = await _teachers.Find(t => t.Email == teacher.Email).FirstOrDefaultAsync();
            if (existingEmail != null && existingEmail.Id != teacher.Id)
                return Result<Teacher>.Failure(TeacherErrors.ConflictEmail());

            await _teachers.ReplaceOneAsync(t => t.Id == teacher.Id, teacher);

            return Result<Teacher>.Success(teacher);

        }
    }
}
