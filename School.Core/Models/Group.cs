using School.Core.Entities;
using System;
namespace School.Core.Models
{
    public class Group
    {
        public required string Id { get; set; }

        public required string Name { get; set; } = string.Empty;

        public int CountOfStudents { get; set; }
    }
}
