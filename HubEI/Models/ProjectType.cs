using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HubEI.Models
{
    /// <summary>
    /// Classe que representa um tipo de projecto.
    /// </summary>
    /// <remarks></remarks>
    public partial class ProjectType
    {
        public ProjectType()
        {
            Project = new HashSet<Project>();
        }

        /// <summary>
        /// Chave primária do tipo de projecto.
        /// </summary>
        /// <value>Chave primária do tipo de projecto.</value>
        public long IdProjectType { get; set; }

        /// <summary>
        /// Descrição do tipo de projecto.
        /// </summary>
        /// <value>Descrição do tipo de projecto.</value>
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<Project> Project { get; set; }
    }
}
