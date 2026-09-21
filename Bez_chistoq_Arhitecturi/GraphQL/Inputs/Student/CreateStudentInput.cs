using System.ComponentModel.DataAnnotations;

namespace School.API.GraphQL.Inputs.Student;

public class CreateStudentInput
{
    [Required]
    [StringLength(50)]
    public required string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public required string LastName { get; set; }

    [Range(6, 18)]
    public int Age { get; set; }

    public required string GroupId { get; set; }
}