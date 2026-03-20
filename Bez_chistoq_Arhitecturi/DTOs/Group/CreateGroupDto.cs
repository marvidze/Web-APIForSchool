using System.ComponentModel.DataAnnotations;

namespace School.API.DTOs.Group
{
    public class CreateGroupDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
