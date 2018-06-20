using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HubEI.Models
{
    /// <summary>
    /// Classe que representa a relação entre projecto e documento.
    /// </summary>
    /// <remarks></remarks>
    public partial class ProjectDocument
    {
        /// <summary>
        /// Chave primária da relação entre projecto e tecnologia.
        /// </summary>
        /// <value>Chave primária da relação entre projecto e tecnologia.</value>
        public long IdProjectDocument { get; set; }

        /// <summary>
        /// Documento presente no projecto.
        /// </summary>
        /// <value>Documento presente no projecto.</value>
        public byte[] Document { get; set; }

        /// <summary>
        /// Chave primária do projecto
        /// </summary>
        /// <value>Chave primária do projecto.</value>
        public long IdProject { get; set; }

        /// <summary>
        /// Nome do documento.
        /// </summary>
        /// <value>Nome do documento.</value>
        [Display(Name = "Name")]
        public string FileName { get; set; }

        /// <summary>
        /// Tamanho do documento.
        /// </summary>
        /// <value>Tamanho do documento.</value>
        [Display(Name = "Size")]
        public double FileSize { get; set; }

        public Project IdProjectNavigation { get; set; }
    }
}
