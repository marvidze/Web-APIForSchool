using System.ComponentModel.DataAnnotations;

namespace School.API.GraphQL.Inputs.Group;

public class UpdateGroupInput
{
    [Required]
    public required string Id { get; set; }

    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
}