using System.ComponentModel.DataAnnotations;

namespace School.API.GraphQL.Inputs.Student;

public class UpdateStudentGroupInput
{
    [Required]
    public required string StudentId { get; set; }

    [Required]
    public required string GroupId { get; set; }
}