using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HubEI.Models
{
    /// <summary>
    /// Classe que representa do ramo de um estudante.
    /// </summary>
    /// <remarks></remarks>
    public partial class StudentBranch
    {
        public StudentBranch()
        {
            Student = new HashSet<Student>();
        }

        /// <summary>
        /// Chave primária do ramo do estudante.
        /// </summary>
        /// <value>Chave primária ramo do estudante.</value>
        public long IdStudentBranch { get; set; }

        /// <summary>
        /// Descrição do ramo.
        /// </summary>
        /// <value>Descrição do ramo.</value>
        [Display(Name = "Branch")]
        public string Description { get; set; }

        [JsonIgnore] 
        public ICollection<Student> Student { get; set; }
    }
}
