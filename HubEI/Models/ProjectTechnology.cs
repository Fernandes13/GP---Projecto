using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HubEI.Models
{
    /// <summary>
    /// Classe que representa a relação entre Projecto e Tecnologia.
    /// </summary>
    /// <remarks></remarks>
    public partial class ProjectTechnology
    {
        /// <summary>
        /// Chave estrangeira da <see cref="HubEI.Models.Project" /> do estudante.
        /// </summary>
        /// <value>Chave estrangeira da morada do estudante.</value>
        public long IdProject { get; set; }

        /// <summary>
        /// Chave estrangeira da <see cref="HubEI.Models.Technology" /> do estudante.
        /// </summary>
        /// <value>Chave estrangeira da morada do estudante.</value>
        public long IdTechnology { get; set; }

        [JsonIgnore]
        public Project IdProjectNavigation { get; set; }

        public Technology IdTechnologyNavigation { get; set; }
    }
}
