using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HubEI.Models
{
    /// <summary>
    /// Classe que representa a relação entre Projecto e Orientador.
    /// </summary>
    /// <remarks></remarks>
    public partial class ProjectAdvisor
    {
        /// <summary>
        /// Chave estrangeira da <see cref="HubEI.Models.Project" /> do estudante.
        /// </summary>
        /// <value>Chave estrangeira da morada do estudante.</value>
        public long IdProject { get; set; }

        /// <summary>
        /// Chave estrangeira da <see cref="HubEI.Models.SchoolMentor" /> do estudante.
        /// </summary>
        /// <value>Chave estrangeira da morada do estudante.</value>
        public long IdSchoolMentor { get; set; }

        [JsonIgnore]
        public Project IdProjectNavigation { get; set; }

        public SchoolMentor IdSchoolMentorNavigation { get; set; }
    }
}
