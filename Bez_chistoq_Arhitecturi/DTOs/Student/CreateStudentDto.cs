using System.ComponentModel.DataAnnotations;

namespace School.API.DTOs.Student
{
    public class CreateStudentDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Range (6, 18)]
        public int Age { get; set; }

        public string GroupId { get; set; } = string.Empty;
    }
}
