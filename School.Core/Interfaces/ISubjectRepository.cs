using School.Core.Common;
using School.Core.Entities;
using School.Core.Models;

namespace School.Core.Interfaces
{
    public interface ISubjectRepository
    {
        Task<Result<Subject>> GetByIdAsync(string id);

        Task<Result<IEnumerable<Subject>>> GetAllAsync();

        Task<Result<Subject>> CreateAsync(Subject subject);

        Task<Result<Subject>> UpdateAsync(Subject subject);

        Task<Result<bool>> DeleteAsync(string id);
    }
}
