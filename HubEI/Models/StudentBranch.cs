using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HubEI.Models
{
    public partial class StudentBranch
    {
        public StudentBranch()
        {
            Student = new HashSet<Student>();
        }

        public long IdStudentBranch { get; set; }

        [Display(Name = "Ramo")]
        public string Description { get; set; }

        public ICollection<Student> Student { get; set; }
    }
}
