using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HubEI.Models
{
    /// <summary>
    /// Classe que representa uma empresa registada na aplicação.
    /// </summary>
    /// <remarks></remarks>
    public partial class Company
    {
        public Company()
        {
            Project = new HashSet<Project>();
        }

        /// <summary>
        /// Chave primária da empresa registado.
        /// </summary>
        /// <value>Chave primária da empresa registado.</value>
        public long IdCompany { get; set; }

        /// <summary>
        /// Nome da empresa.
        /// </summary>
        /// <value>Nome da empresa.</value>
        public string Name { get; set; }

        /// <summary>
        /// Descrição da empresa.
        /// </summary>
        /// <value>Descrição da empresa.</value>
        public string Description { get; set; }

        /// <summary>
        /// Chave estrangeira da <see cref="HubEI.Models.District" /> do estudante.
        /// </summary>
        /// <value>Chave estrangeira da morada do estudante.</value>
        [Display(Name = "District")]
        public long IdDistrict { get; set; }

        [Display(Name = "District")]
        public District IdDistrictNavigation { get; set; }

        [JsonIgnore]
        public ICollection<Project> Project { get; set; }
    }
}
