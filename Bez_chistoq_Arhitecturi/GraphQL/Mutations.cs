using School.API.GraphQL.Inputs.Group;
using School.API.GraphQL.Inputs.Student;
using School.Application.Contracts.Group;
using School.Application.Contracts.Student;
using School.Application.Interfaces;
using School.Core.Entities;
using School.Core.Models;
using School.API.GraphQL.Validation;

namespace School.API.GraphQL;

public class Mutation
{
    public async Task<Student> CreateStudent(CreateStudentInput input, IStudentService studentService)
    {
        InputValidator.Validate(input);

        var request = new CreateStudentRequest
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            Age = input.Age,
            GroupId = input.GroupId
        };

        var result = await studentService.CreateAsync(request);

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

    public async Task<Student> UpdateStudent(UpdateStudentInput input, IStudentService studentService)
    {
        InputValidator.Validate(input);

        var request = new UpdateStudentRequest
        {
            Id = input.Id,
            FirstName = input.FirstName,
            LastName = input.LastName,
            Age = input.Age,
            GroupId = input.GroupId
        };

        var result = await studentService.UpdateAsync(request);

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

    public async Task<Student> UpdateStudentGroup(UpdateStudentGroupInput input, IStudentService studentService)
    {
        InputValidator.Validate(input);

        var request = new UpdateStudentGroupRequest
        {
            StudentId = input.StudentId,
            GroupId = input.GroupId
        };

        var result = await studentService.UpdateStudentGroupAsync(request);

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

    public async Task<bool> DeleteStudent(string id, IStudentService studentService)
    {
        var result = await studentService.DeleteAsync(id);

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

    public async Task<Group> CreateGroup(CreateGroupInput input, IGroupService groupService)
    {
        InputValidator.Validate(input);

        var request = new CreateGroupRequest
        {
            Name = input.Name
        };

        var result = await groupService.CreateAsync(request);

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

    public async Task<Group> UpdateGroup(UpdateGroupInput input, IGroupService groupService)
    {
        InputValidator.Validate(input);

        var request = new UpdateGroupRequest
        {
            Id = input.Id,
            Name = input.Name
        };

        var result = await groupService.UpdateAsync(request);

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

    public async Task<bool> DeleteGroup(string id, IGroupService groupService)
    {
        var result = await groupService.DeleteAsync(id);

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