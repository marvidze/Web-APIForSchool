using School.Application.Interfaces;
using School.Core.Entities;
using School.Core.Models;

namespace School.API.GraphQL;

[ExtendObjectType(typeof(Group))]
public class GroupResolvers
{
    public async Task<IEnumerable<Student>> GetStudents([Parent] Group group, IStudentService studentService)
    {
        var result = await studentService.GetByGroupIdAsync(group.Id);

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