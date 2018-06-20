using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HubEI.Models
{
    /// <summary>
    /// Classe que representa um estudante registado na aplicação.
    /// </summary>
    /// <remarks></remarks>
    public partial class Student
    {
        public Student()
        {
            Project = new HashSet<Project>();
        }

        /// <summary>
        /// Chave primária do estudante.
        /// </summary>
        /// <value>Chave primária do estudante registado.</value>
        public long IdStudent { get; set; }

        /// <summary>
        /// Nome do estudante.
        /// Deverá conter, no máximo, 31 caracteres.
        /// </summary>
        /// <value>Nome do estudante.</value>
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Contacto de telefone do estudante.
        /// Tem de ser expresso em 9 ou 11 algarismos.
        /// </summary>
        /// <value>Contacto de telefone do estudante.</value>
        [Display(Name = "Contact")]
        [DataType(DataType.Text)]
        public long? Telephone { get; set; }

        /// <summary>
        /// Data de nascimento do estudante.
        /// </summary>
        /// <value>Data de nascimento do estudante.</value>
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Número do IPS do estudante.
        /// Tem de ser expresso em 9 algarismos.
        /// </summary>
        /// <value>Número do IPS do estudante.</value>
        [Display(Name = "Student Number")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Student number must be a number")]
        [Range(0, int.MaxValue, ErrorMessage = "Student number must be a number")]
        public long StudentNumber { get; set; }

        /// <summary>
        /// Chave estrangeira da <see cref="HubEI.Models.StudentBranch" /> do estudante.
        /// </summary>
        /// <value>Chave estrangeira da morada do estudante.</value>
        public long IdStudentBranch { get; set; }

        /// <summary>
        /// Chave estrangeira da <see cref="HubEI.Models.Address" /> do estudante.
        /// </summary>
        /// <value>Chave estrangeira da morada do estudante.</value>
        public long IdAddress { get; set; }

        public Address IdAddressNavigation { get; set; }

        [Display(Name = "Branch")]
        public StudentBranch IdStudentBranchNavigation { get; set; }

        [JsonIgnore]
        public ICollection<Project> Project { get; set; }
    }
}
