using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Entities
{
    public class Student
    {
        public required string Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public int Age { get; set; }

        public string? GroupId { get; set; }
    }
}
