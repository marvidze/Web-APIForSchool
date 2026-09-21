using System.ComponentModel.DataAnnotations;

namespace School.API.GraphQL.Inputs.Group;

public class CreateGroupInput
{
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
}