using School.Application.Interfaces;
using School.Core.Entities;
using School.Core.Models;

namespace School.API.GraphQL;


[ExtendObjectType(typeof(Student))]
public class StudentResolvers
{
    public async Task<Group?> GetGroup([Parent] Student student, IGroupService groupService)
    {
        if (student.GroupId is null)
        {
            return null;
        }

        var result = await groupService.GetByIdAsync(student.GroupId);

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

        return result.Data;
    }
}