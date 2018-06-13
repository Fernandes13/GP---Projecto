using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HubEI.Models
{
    public partial class Student
    {
        public Student()
        {
            Project = new HashSet<Project>();
        }

        public long IdStudent { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Contact")]
        [DataType(DataType.Text)]
        public long? Telephone { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Student Number")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Student number must be a number")]
        [Range(0, int.MaxValue, ErrorMessage = "Student number must be a number")]
        public long StudentNumber { get; set; }


        public long IdStudentBranch { get; set; }
        public long IdAddress { get; set; }

        public Address IdAddressNavigation { get; set; }

        [Display(Name = "Branch")]
        public StudentBranch IdStudentBranchNavigation { get; set; }

        [JsonIgnore]
        public ICollection<Project> Project { get; set; }
    }
}
