using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Título")]
        public string Title { get; set; }
        [Display(Name = "Descrição")]
        public string Description { get; set; }
        [Display(Name = "Relatório")]
        public byte[] Report { get; set; }
        [Display(Name = "Data")]
        public DateTime ProjectDate { get; set; }
        [Display(Name = "Visível")]
        public byte IsVisible { get; set; }
        [Display(Name = "Visualizações")]
        public int Views { get; set; }
        [Display(Name = "Transferências")]
        public int Downloads { get; set; }
        public long IdProjectType { get; set; }
        public long IdStudent { get; set; }
        public long IdCompany { get; set; }

        public Company IdCompanyNavigation { get; set; }
        public ProjectType IdProjectTypeNavigation { get; set; }
        public Student IdStudentNavigation { get; set; }
        public ICollection<ProjectAdvisor> ProjectAdvisor { get; set; }
        public ICollection<ProjectDocument> ProjectDocument { get; set; }
        public ICollection<ProjectTechnology> ProjectTechnology { get; set; }
    }
}
