using School.Core.Common;
using School.Core.Entities;

namespace School.Core.Interfaces
{
    public interface IStudentRepository
    {
        Task<Result<Student>> GetByIdAsync(string id);

        Task<Result<IEnumerable<Student>>> GetByGroupIdAsync(string groupId);

        Task<Result<IEnumerable<Student>>> GetAllAsync();

        Task<Result<Student>> CreateAsync(Student student);

        Task<Result<Student>> UpdateAsync(Student student);

        Task<Result<bool>> DeleteAsync(string id);
    }
}
