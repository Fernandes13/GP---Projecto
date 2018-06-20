using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubEI.Models
{
    /// <summary>
    /// Classe que representa o regulamento geral de protecção de dados.
    /// </summary>
    /// <remarks></remarks>
    public class RgpdInfo
    {
        /// <summary>
        /// Chave primária do regulamento registado.
        /// </summary>
        /// <value>Chave primária do regulamento registado.</value>
        public long IdTerm { get; set; }

        /// <summary>
        /// Descrição do regulamento.
        /// </summary>
        /// <value>Descrição do regulamento.</value>
        public string Description { get; set; }
    }
}
