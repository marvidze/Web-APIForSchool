using School.Core.Common;
using School.Core.Entities;
using School.Core.Models;

namespace School.Core.Interfaces
{
    public interface ITeacherRepository
    {
        Task<Result<Teacher>> GetByIdAsync(string  id);

        Task<Result<IEnumerable<Teacher>>> GetAllAsync();

        Task<Result<Teacher>> CreateAsync(Teacher teacher);

        Task<Result<Teacher>> UpdateAsync(Teacher teacher);

        Task<Result<bool>> DeleteAsync(string id);
    }
}
