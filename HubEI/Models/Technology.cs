using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HubEI.Models
{
    /// <summary>
    /// Classe que representa uma tecnologia.
    /// </summary>
    /// <remarks></remarks>
    public partial class Technology
    {
        public Technology()
        {
            ProjectTechnology = new HashSet<ProjectTechnology>();
        }

        /// <summary>
        /// Chave primária da tecnologia registada.
        /// </summary>
        /// <value>Chave primária da tecnologia registada.</value>
        public long IdTechnology { get; set; }

        /// <summary>
        /// Descrição de uma tecnologia.
        /// </summary>
        /// <value>Descrição de uma tecnologia.</value>
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<ProjectTechnology> ProjectTechnology { get; set; }
    }
}
