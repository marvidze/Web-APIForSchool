using System.ComponentModel.DataAnnotations;

namespace School.API.DTOs.Group
{
    public class UpdateGroupDto
    {
        [Required]
        public string Id { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
