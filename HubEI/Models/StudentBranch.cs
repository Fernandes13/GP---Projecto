using System;
using System.Collections.Generic;

namespace HubEI.Models
{
    public partial class StudentBranch
    {
        public StudentBranch()
        {
            Student = new HashSet<Student>();
        }

        public long IdStudentBranch { get; set; }
        public string Description { get; set; }

        public ICollection<Student> Student { get; set; }
    }
}
