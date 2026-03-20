namespace School.Application.Contracts.Student
{
    public class UpdateStudentGroupRequest
    {
        public string StudentId { get; set; } = string.Empty;

        public string GroupId { get; set; } = string.Empty;
    }
}
