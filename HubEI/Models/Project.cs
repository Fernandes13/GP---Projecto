using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CIMOB_IPS.Models.CustomValidations;
using HubEI.Models.CustomValidations;

namespace HubEI.Models
{
    /// <summary>
    /// Classe que representa um projecto registado na aplicação.
    /// </summary>
    /// <remarks></remarks>
    public partial class Project
    {
        public Project()
        {
            ProjectAdvisor = new HashSet<ProjectAdvisor>();
            ProjectDocument = new HashSet<ProjectDocument>();
            ProjectTechnology = new HashSet<ProjectTechnology>();
        }

        /// <summary>
        /// Chave primária do projecto registado.
        /// </summary>
        /// <value>Chave primária do projecto registado.</value>
        public long IdProject { get; set; }

        /// <summary>
        /// Título do projecto.
        /// </summary>
        /// <value>Título do projecto.</value>
        [Display(Name = "Title")]
        public string Title { get; set; }

        /// <summary>
        /// Descrição do projecto.
        /// </summary>
        /// <value>Descrição do projecto.</value>
        [Required(ErrorMessage = "You need to set the project's description!")]
        [Display(Name = "Description")]
        [MaxLength(999)]
        public string Description { get; set; }

        /// <summary>
        /// Relatório do projecto.
        /// </summary>
        /// <value>Relatório do projecto.</value>
        [Display(Name = "Report")]
        public byte[] Report { get; set; }

        /// <summary>
        /// Data de submissão do projecto.
        /// </summary>
        /// <value>Data de submissão do projecto.</value>
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "You need to set the project's date!")]
        [DataType(DataType.Date)]
        [IsDateBeforeToday]
        public DateTime ProjectDate { get; set; }

        /// <summary>
        /// Variável de controlo para tornar o projecto visivel ou não.
        /// </summary>
        /// <value>Variável de controlo para tornar o projecto visivel ou não.</value>
        [Display(Name = "Is Visible")]
        public bool IsVisible { get; set; }

        /// <summary>
        /// Nota do projecto.
        /// </summary>
        /// <value>Nota do projecto.</value>
        [Display(Name = "Grade")]
        public short Grade { get; set; }

        /// <summary>
        /// Chave estrangeira da <see cref="HubEI.Models.ProjectType" /> do estudante.
        /// </summary>
        /// <value>Chave estrangeira da morada do estudante.</value>
        [Display(Name = "Type of Project")]
        [Required(ErrorMessage = "You need to set the project's type!")]
        public long IdProjectType { get; set; }

        /// <summary>
        /// Chave estrangeira da <see cref="HubEI.Models.Student" /> do estudante.
        /// </summary>
        /// <value>Chave estrangeira da morada do estudante.</value>
        [Display(Name = "Student")]
        [Required(ErrorMessage = "You need to set the project's author!")]
        public long IdStudent { get; set; }

        /// <summary>
        /// Chave estrangeira da <see cref="HubEI.Models.Company" /> do estudante.
        /// </summary>
        /// <value>Chave estrangeira da morada do estudante.</value>
        [Display(Name = "Company")]
        public long IdCompany { get; set; }

        /// <summary>
        /// Número de visualizações do projecto.
        /// </summary>
        /// <value>Número de visualizações do projecto.</value>
        [Display(Name = "Views")]
        public int Views { get; set; }

        /// <summary>
        /// Número de dowloands do projecto.
        /// </summary>
        /// <value>Número de dowloands do projecto.</value>
        [Display(Name = "Downloads")]
        public int Downloads { get; set; }

        [Display(Name = "Business Area")]
        public long IdBusinessArea { get; set; }

        [Display(Name = "Video")]
        public byte[] Video { get; set; }

        public Company IdCompanyNavigation { get; set; }
        public ProjectType IdProjectTypeNavigation { get; set; }
        public Student IdStudentNavigation { get; set; }
        public BusinessArea IdBusinessAreaNavigation { get; set; }
        public ICollection<ProjectAdvisor> ProjectAdvisor { get; set; }
        public ICollection<ProjectDocument> ProjectDocument { get; set; }
        public ICollection<ProjectTechnology> ProjectTechnology { get; set; }
    }
}
