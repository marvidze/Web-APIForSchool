using School.Application.Contracts.Student;
using School.Core.Common;
using School.Core.Entities;

namespace School.Application.Interfaces
{
    public interface IStudentService
    {
        Task<Result<Student>> GetByIdAsync(string id);

        Task<Result<IEnumerable<Student>>> GetAllAsync();

        Task<Result<IEnumerable<Student>>> GetByGroupIdAsync(string groupId);

        Task<Result<Student>> CreateAsync(CreateStudentRequest student);

        Task<Result<Student>> UpdateAsync(UpdateStudentRequest student);

        Task<Result<bool>> DeleteAsync(string id);

        Task<Result<Student>> UpdateStudentGroupAsync(UpdateStudentGroupRequest addStudentGroup);
    }
}
