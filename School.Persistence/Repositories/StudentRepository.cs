using MongoDB.Driver;
using School.Core.Common;
using School.Core.Entities;
using School.Core.Errors;
using School.Core.Interfaces;


namespace School.Persistence.Repositories
{
    public class StudentRepository : IStudentRepository
    {

        private readonly IMongoCollection<Student> _studentsCollection;

        public StudentRepository(MongoDbContext dbContext) 
        {
            _studentsCollection = dbContext.GetCollection<Student>();
        }

        public async Task<Result<Student>> GetByIdAsync(string id)
        {
            var student = await _studentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (student == null) return Result<Student>.Failure(StudentErrors.NotFound(id));

            return Result<Student>.Success(student);
        }

        public async Task<Result<IEnumerable<Student>>> GetByGroupIdAsync(string groupId)
        {
            var students = await _studentsCollection.Find(x => x.GroupId == groupId).ToListAsync();

            return Result<IEnumerable<Student>>.Success(students); 
        }

        public async Task<Result<IEnumerable<Student>>> GetAllAsync()
        {
            var students = await _studentsCollection.Find(_ => true).ToListAsync();

            return Result<IEnumerable<Student>>.Success(students);
        }

        public async Task<Result<Student>> CreateAsync(Student student)
        {
            await _studentsCollection.InsertOneAsync(student);

            return Result<Student>.Success(student);
        }

        public async Task<Result<Student>> UpdateAsync(Student student)
        {
            var isExist = await GetByIdAsync(student.Id);
            if (isExist.IsFailure) return Result<Student>.Failure(StudentErrors.NotFound(student.Id));
            
            await _studentsCollection.ReplaceOneAsync(s => s.Id == student.Id, student);

            return Result<Student>.Success(student);
        }

        public async Task<Result<bool>> DeleteAsync(string id)
        {
            var isExist = await GetByIdAsync(id);
            if (isExist.IsFailure) return Result<bool>.Failure(StudentErrors.NotFound(id));

            await _studentsCollection.DeleteOneAsync(s => s.Id == id);

            return Result<bool>.Success(true);
        }
    }
}
