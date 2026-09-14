namespace School.Core.Models
{
    public class Teacher
    {
        public required string Id { get; set; }
        
        public required string FirstName { get; set; }

        public required string LastName { get; set; } 

        public required string Email {  get; set; }

        public required DateTime HireDate { get; set; }

        public string? Specialization { get; set; }

        public List<string>? SubjectIds { get; set; }

        public string? GroupId { get; set; }
    }
}
