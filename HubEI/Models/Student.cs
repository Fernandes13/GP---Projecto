using System;
using System.Collections.Generic;

namespace HubEI.Models
{
    public partial class Student
    {
        public Student()
        {
            Project = new HashSet<Project>();
        }

        public long IdStudent { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long? Telephone { get; set; }
        public DateTime BirthDate { get; set; }
        public long StudentNumber { get; set; }
        public long IdStudentBranch { get; set; }
        public long IdAddress { get; set; }

        public Address IdAddressNavigation { get; set; }
        public StudentBranch IdStudentBranchNavigation { get; set; }
        public ICollection<Project> Project { get; set; }
    }
}
