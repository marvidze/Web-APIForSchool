using School.Application.Interfaces;
using School.Core.Entities;
using School.Core.Models;

namespace School.API.GraphQL;

public class Query
{
    public async Task<IEnumerable<Student>> GetStudents(IStudentService studentService)
    {
        var result = await studentService.GetAllAsync();

        return result.Data!;
    }

    public async Task<Student> GetStudent( string id, IStudentService studentService)
    {
        var result = await studentService.GetByIdAsync(id);

        if (result.IsFailure)
        {
            throw new GraphQLException(
                ErrorBuilder.New()
                .SetMessage(result.Error!.Message)
                .SetCode(result.Error.Code)
                .SetExtension("statusCode", result.Error.StatusCode)
                .Build()
            );
        }

        return result.Data!;
    }

    public async Task<IEnumerable<Group>> GetGroups(IGroupService groupService)
    {
        var result = await groupService.GetAllAsync();

        if (result.IsFailure)
        {
            throw new GraphQLException(
                ErrorBuilder.New()
                    .SetMessage(result.Error!.Message)
                    .SetCode(result.Error.Code)
                    .SetExtension("statusCode", result.Error.StatusCode)
                    .Build()
            );
        }

        return result.Data!;
    }

    public async Task<Group> GetGroup(string id, IGroupService groupService)
    {
        var result = await groupService.GetByIdAsync(id);

        if (result.IsFailure)
        {
            throw new GraphQLException(
                ErrorBuilder.New()
                    .SetMessage(result.Error!.Message)
                    .SetCode(result.Error.Code)
                    .SetExtension("statusCode", result.Error.StatusCode)
                    .Build()
            );
        }

        return result.Data!;
    }
}