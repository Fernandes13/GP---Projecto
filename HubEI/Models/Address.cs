using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HubEI.Models
{
    /// <summary>
    /// Classe que representa uma morada registada na aplicação.
    /// </summary>
    /// <remarks></remarks>
    public partial class Address
    {
        public Address()
        {
            Company = new HashSet<Company>();
            Student = new HashSet<Student>();
        }

        /// <summary>
        /// Chave primária da morada registado.
        /// </summary>
        /// <value>Chave primária da morada registado.</value>
        public long IdAddress { get; set; }

        /// <summary>
        /// Código Postal da morada.
        /// </summary>
        /// <value>Código Postal da morada.</value>
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Rua da morada.
        /// </summary>
        /// <value>Rua da morada.</value>
        [Display(Name = "Address")]
        public string Address1 { get; set; }

        /// <summary>
        /// Número de porta da morada.
        /// </summary>
        /// <value>Número de porta da morada.</value>
        [Display(Name = "Door")]
        public string Door { get; set; }

        /// <summary>
        /// Localidade da morada.
        /// </summary>
        /// <value>Localidade da morada.</value>
        [Display(Name = "Locality")]
        public string Locality { get; set; }

        /// <summary>
        /// Chave estrangeira da <see cref="HubEI.Models.District" /> do estudante.
        /// </summary>
        /// <value>Chave estrangeira da morada do estudante.</value>
        public long IdDistrict { get; set; }

        [Display(Name = "District")]
        public District IdDistrictNavigation { get; set; }
        public ICollection<Company> Company { get; set; }

        [JsonIgnore] 
        public ICollection<Student> Student { get; set; }
    }
}
