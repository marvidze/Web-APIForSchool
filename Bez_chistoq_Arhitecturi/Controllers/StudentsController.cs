using Microsoft.AspNetCore.Mvc;
using School.API.DTOs.Student;
using School.Application.Contracts.Student;
using School.Application.Interfaces;

namespace School.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll()
        {
            var result = await _studentService.GetAllAsync();

            if (result.IsFailure) return StatusCode(result.Error.StatusCode);

            var students = result.Data;

            var studentDtos = students.Select(s => new StudentDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Age = s.Age,
                GroupId = s.GroupId
            });

            return Ok(studentDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetById(string id)
        {
            var result = await _studentService.GetByIdAsync(id);

            if (result.IsFailure) return Problem(title: result.Error.Message, statusCode: result.Error.StatusCode);

            var student = result.Data;

            var studentDtos = new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                GroupId = student.GroupId,
            };

            return Ok(studentDtos);
        }

        [HttpGet("by-group/{groupId}")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetByGroupId(string groupId)
        {
            var result = await _studentService.GetByGroupIdAsync(groupId);

            if (result.IsFailure) return Problem(title: result.Error.Message, statusCode: result.Error.StatusCode);

            var students = result.Data;

            var studentDtos = students.Select(s => new StudentDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Age = s.Age,
                GroupId = s.GroupId
            });

            return Ok(studentDtos);
        }

        [HttpPost]
        public async Task<ActionResult<StudentDto>> Create([FromBody] CreateStudentDto createStundent)
        {
            var studentRequest = new CreateStudentRequest
            {
                LastName = createStundent.LastName,
                FirstName = createStundent.FirstName,
                Age = createStundent.Age,
                GroupId = createStundent.GroupId
            };

            var result = await _studentService.CreateAsync(studentRequest);
            if (result.IsFailure) return Problem(title: result.Error.Message, statusCode: result.Error.StatusCode);

            var student = result.Data;

            var studentDto = new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                GroupId = student.GroupId
            };

            return CreatedAtAction(nameof(GetById), new {id = student.Id}, studentDto);
        }

        [HttpPut("{studentId}/group/{groupId}")]
        public async Task<ActionResult<StudentDto>> UpdateGroup(string studentId, string groupId)
        {
            var updateStudentGroupRequest = new UpdateStudentGroupRequest
            {
                GroupId = groupId,
                StudentId = studentId
            };

            var result = await _studentService.UpdateStudentGroupAsync(updateStudentGroupRequest);
            if (result.IsFailure) return Problem(title: result.Error.Message, statusCode: result.Error.StatusCode);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] UpdateStudentDto updateStudent)
        {
            if (id != updateStudent.Id)
                return Problem(title: "ID в URL не совпадает с ID в запросе", statusCode: 400);

            var studentRequest = new UpdateStudentRequest
            {
                Id = updateStudent.Id,
                FirstName = updateStudent.FirstName,
                LastName = updateStudent.LastName,
                Age = updateStudent.Age,
                GroupId = updateStudent.GroupId
            };

            var result = await _studentService.UpdateAsync(studentRequest);

            if (result.IsFailure) return Problem(title: result.Error.Message, statusCode: result.Error.StatusCode);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _studentService.DeleteAsync(id);

            if (result.IsFailure) return Problem(title: result.Error.Message, statusCode: result.Error.StatusCode);

            return NoContent();
        }

        
    }
}
