namespace School.Core.Models
{
    public class Subject
    {
        public required string Id { get; set; }

        public required string Name { get; set; }

        public string? Description {  get; set; }

        public int? Credits { get; set; }
    }
}
