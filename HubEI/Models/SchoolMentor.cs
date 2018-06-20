using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HubEI.Models
{
    /// <summary>
    /// Classe que representa um orientador.
    /// </summary>
    /// <remarks></remarks>
    public partial class SchoolMentor
    {
        public SchoolMentor()
        {
            ProjectAdvisor = new HashSet<ProjectAdvisor>();
        }

        /// <summary>
        /// Chave primária do orientador.
        /// </summary>
        /// <value>Chave primária do orientador.</value>
        public long IdSchoolMentor { get; set; }

        /// <summary>
        /// Nome do orientador.
        /// Deverá conter, no máximo, 31 caracteres.
        /// </summary>
        /// <value>Nome do estudante.</value>
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Email do orientador.
        /// Deverá ser um email válido.
        /// </summary>
        /// <value>Email do orientador.</value>
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Field has to be an email")]
        public string Email { get; set; }

        [JsonIgnore]
        public ICollection<ProjectAdvisor> ProjectAdvisor { get; set; }
    }
}
