using HotChocolate;
using School.API.GraphQL.DataLoaders;
using School.Core.Entities;
using School.Core.Errors;
using School.Core.Models;

namespace School.API.GraphQL;

[ExtendObjectType(typeof(Student))]
public class StudentResolvers
{
    public async Task<Group?> GetGroup([Parent] Student student, GroupByIdDataLoader groupDataLoader)
    {
        if (student.GroupId is null)
        {
            return null;
        }

        var group = await groupDataLoader.LoadAsync(student.GroupId);

        if (group is null)
        {
            var error = GroupErrors.NotFound(student.GroupId);

            throw new GraphQLException(
                ErrorBuilder.New()
                    .SetMessage(error.Message)
                    .SetCode(error.Code)
                    .SetExtension("statusCode", error.StatusCode)
                    .Build());
        }

        return group;
    }
}