﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HubEI.Models
{
    public partial class Student
    {
        public Student()
        {
            Project = new HashSet<Project>();
        }

        public long IdStudent { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Contacto")]
        public long? Telephone { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Nascimento")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Número de Estudante")]
        public long StudentNumber { get; set; }
        public long IdStudentBranch { get; set; }
        public long IdAddress { get; set; }

        public Address IdAddressNavigation { get; set; }
        public StudentBranch IdStudentBranchNavigation { get; set; }
        public ICollection<Project> Project { get; set; }
    }
}
