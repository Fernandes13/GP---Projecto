using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CIMOB_IPS.Models.CustomValidations;
using HubEI.Models.CustomValidations;

namespace HubEI.Models
{
    public partial class Project
    {
        public Project()
        {
            ProjectAdvisor = new HashSet<ProjectAdvisor>();
            ProjectDocument = new HashSet<ProjectDocument>();
            ProjectTechnology = new HashSet<ProjectTechnology>();
        }

        public long IdProject { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "You need to set the project's description!")]
        [Display(Name = "Description")]
        [MaxLength(999)]
        public string Description { get; set; }

        [Display(Name = "Report")]
        public byte[] Report { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "You need to set the project's date!")]
        [DataType(DataType.Date)]
        [IsDateBeforeToday]
        public DateTime ProjectDate { get; set; }

        [Display(Name = "Is Visible")]
        public bool IsVisible { get; set; }

        [Display(Name = "Grade")]
        public short Grade { get; set; }

        [Display(Name = "Type of Project")]
        [Required(ErrorMessage = "You need to set the project's type!")]
        public long IdProjectType { get; set; }

        [Display(Name = "Student")]
        [Required(ErrorMessage = "You need to set the project's author!")]
        public long IdStudent { get; set; }

        [Display(Name = "Company")]
        public long IdCompany { get; set; }

        [Display(Name = "Views")]
        public int Views { get; set; }

        [Display(Name = "Downloads")]
        public int Downloads { get; set; }

        public long IdBusinessArea { get; set; }

        public Company IdCompanyNavigation { get; set; }
        public ProjectType IdProjectTypeNavigation { get; set; }
        public Student IdStudentNavigation { get; set; }
        public BusinessArea IdBusinessAreaNavigation { get; set; }
        public ICollection<ProjectAdvisor> ProjectAdvisor { get; set; }
        public ICollection<ProjectDocument> ProjectDocument { get; set; }
        public ICollection<ProjectTechnology> ProjectTechnology { get; set; }
    }
}
