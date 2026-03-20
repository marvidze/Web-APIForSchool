using School.Application.Contracts.Student;
using School.Application.Interfaces;
using School.Core.Common;
using School.Core.Entities;
using School.Core.Errors;
using School.Core.Interfaces;

namespace School.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;

        public StudentService(IStudentRepository studentRepository, IGroupRepository groupRepository)
        {
            _studentRepository = studentRepository; 
            _groupRepository = groupRepository;
        }

        public async Task<Result<Student>> GetByIdAsync(string id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        public async Task<Result<IEnumerable<Student>>> GetAllAsync()
        {
            return await _studentRepository.GetAllAsync();
        }

        public async Task<Result<IEnumerable<Student>>> GetByGroupIdAsync(string groupId)
        {
            return await _studentRepository.GetByGroupIdAsync(groupId);
        }

        public async Task<Result<Student>> CreateAsync(CreateStudentRequest student)
        {
            var newStudent = new Student
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                GroupId = student.GroupId,
            };

            return await _studentRepository.CreateAsync(newStudent);
        }

        public async Task<Result<Student>> UpdateAsync(UpdateStudentRequest student)
        {
            var updateStudent = new Student
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                GroupId = student.GroupId,
            };

            return await _studentRepository.UpdateAsync(updateStudent);
        }

        public async Task<Result<bool>> DeleteAsync(string id)
        {
            return await _studentRepository.DeleteAsync(id);
        }

        public async Task<Result<Student>> UpdateStudentGroupAsync(UpdateStudentGroupRequest studentGroup)
        {
            var studentIsExist = await GetByIdAsync(studentGroup.StudentId);
            if (studentIsExist.IsFailure) return Result<Student>.Failure(StudentErrors.NotFound(studentGroup.StudentId));

            var groupIsExist = await _groupRepository.GetByIdAsync(studentGroup.GroupId);
            if (groupIsExist.IsFailure) return Result<Student>.Failure(GroupErrors.NotFound(studentGroup.GroupId));

            var student = studentIsExist.Data;

            var updateStudentRequest = new UpdateStudentRequest
            {
                Id = student.Id,
                FirstName= student.FirstName,
                LastName= student.LastName,
                Age = student.Age,
                GroupId = studentGroup.GroupId,
            };

            return await UpdateAsync(updateStudentRequest);
        }
    }
}
