using Microsoft.AspNetCore.Mvc;
using School.API.DTOs.Group;
using School.API.DTOs.Student;
using School.Application.Contracts.Group;
using School.Application.Interfaces;

namespace School.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IStudentService _studentService;

        public GroupsController(IGroupService groupService, IStudentService studentService)
        {
            _groupService = groupService;
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetAll()
        {
            var result = await _groupService.GetAllAsync();

            if (result.IsFailure) return Problem(title: result.Error.Message, statusCode: result.Error.StatusCode);

            var groups = result.Data;

            var groupsDto = groups.Select(s => new GroupDto
            {
                Id = s.Id,
                Name = s.Name,
            });

            return Ok(groupsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDto>> GetById(string id)
        {
            var result = await _groupService.GetByIdAsync(id);

            if (result.IsFailure) return Problem(title: result.Error.Message, statusCode: result.Error.StatusCode);

            var group = result.Data;

            var groupDto = new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
            };

            return Ok(groupDto);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateGroupDto createGroup)
        {
            var createGroupRequest = new CreateGroupRequest
            {
                Name = createGroup.Name,
            };

            var result = await _groupService.CreateAsync(createGroupRequest);

            if (result.IsFailure) return Problem(title: result.Error.Message, statusCode: result.Error.StatusCode);

            var group = result.Data;

            var groupDto = new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
            };

            return CreatedAtAction(nameof(GetById), new { id = group.Id }, groupDto);
        }

        [HttpGet("{id}/students")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudentsByGroup(string id)
        {
            var group = await _groupService.GetByIdAsync(id);
            if (group.IsFailure) return Problem(title: group.Error.Message, statusCode:  group.Error.StatusCode);

            var requestStudents = await _studentService.GetByGroupIdAsync(id);
            if (requestStudents.IsFailure) return Problem(title: requestStudents.Error.Message, statusCode: requestStudents.Error.StatusCode);

            var studentsDto = requestStudents.Data.Select(s => new StudentDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Age = s.Age,
                GroupId = s.GroupId
            });
            
            return Ok(studentsDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] UpdateGroupDto updateGroup)
        {
            if (id != updateGroup.Id) return Problem(title: "ID в URL не совпадает с ID в запросе");

            var updateGroupRequest = new UpdateGroupRequest
            {
                Id = updateGroup.Id,
                Name = updateGroup.Name,
            };

            var result = await _groupService.UpdateAsync(updateGroupRequest);

            if (result.IsFailure) return Problem(title: result.Error.Message, statusCode: result.Error.StatusCode);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await _groupService.DeleteAsync(id);

            if (result.IsFailure) return Problem(title: result.Error.Message, statusCode: result.Error.StatusCode);

            return NoContent();
        }
    }
}
