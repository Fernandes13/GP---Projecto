using System;
using System.Collections.Generic;

namespace HubEI.Models
{
    /// <summary>
    /// Classe que representa um distrito registado na aplicação.
    /// </summary>
    /// <remarks></remarks>
    public partial class District
    {
        public District()
        {
            Company = new HashSet<Company>();
            Address = new HashSet<Address>();
        }

        /// <summary>
        /// Chave primária do distrito registado.
        /// </summary>
        /// <value>Chave primária do distrito registado.</value>
        public long IdDistrict { get; set; }

        /// <summary>
        /// Descrição do distrito.
        /// </summary>
        /// <value>Descrição do distrito.</value>
        public string Description { get; set; }

        public ICollection<Address> Address { get; set; }

        public ICollection<Company> Company { get; set; }
    }
}
