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

        [Required(ErrorMessage = "É necessário definir o título do projecto!")]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Required(ErrorMessage = "É necessário definir a descrição do projecto!")]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Display(Name = "Relatório")]
        public byte[] Report { get; set; }

        [Display(Name = "Data")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "É necessário definir a data do projecto!")]
        [DataType(DataType.Date)]
        public DateTime ProjectDate { get; set; }

        [Display(Name = "Visível")]
        public bool IsVisible { get; set; }

        [Display(Name = "Tipo de Projecto")]
        [Required(ErrorMessage = "É necessário definir o tipo de projecto!")]
        public long IdProjectType { get; set; }

        [Display(Name = "Estudante")]
        [Required(ErrorMessage = "É necessário definir o tipo de projecto!")]
        public long IdStudent { get; set; }

        [Display(Name = "Empresa")]
        public long IdCompany { get; set; }

        [Display(Name = "Visualizações")]
        public int Views { get; set; }

        [Display(Name = "Transferências")]
        public int Downloads { get; set; }

        public Company IdCompanyNavigation { get; set; }
        public ProjectType IdProjectTypeNavigation { get; set; }
        public Student IdStudentNavigation { get; set; }
        public ICollection<ProjectAdvisor> ProjectAdvisor { get; set; }
        public ICollection<ProjectDocument> ProjectDocument { get; set; }
        public ICollection<ProjectTechnology> ProjectTechnology { get; set; }
    }
}
