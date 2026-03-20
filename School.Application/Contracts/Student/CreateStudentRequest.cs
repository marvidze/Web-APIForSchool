namespace School.Application.Contracts.Student
{
    public class CreateStudentRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }

        public string GroupId { get; set; } = string.Empty;
    }
}
