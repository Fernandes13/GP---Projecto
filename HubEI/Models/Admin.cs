using System;
using System.Collections.Generic;

namespace HubEI.Models
{
    /// <summary>
    /// Classe que representa um administrador registado na aplicação.
    /// </summary>
    /// <remarks></remarks>
    public partial class Admin
    {
        /// <summary>
        /// Chave primária do administrador registado.
        /// </summary>
        /// <value>Chave primária do administrador registado.</value>
        public long IdAdmin { get; set; }

        /// <summary>
        /// Email do orientador.
        /// Deverá ser um email válido.
        /// </summary>
        /// <value>Email do orientador.</value>
        public string Email { get; set; }

        /// <summary>
        /// Password do orientador.
        /// </summary>
        /// <value>Password do orientador.</value>
        public byte[] Password { get; set; }
    }
}
