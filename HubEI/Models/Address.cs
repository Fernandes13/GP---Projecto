using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HubEI.Models
{
    public partial class Address
    {
        public Address()
        {
            Company = new HashSet<Company>();
            Student = new HashSet<Student>();
        }

        public long IdAddress { get; set; }

        [Display(Name = "Código Postal")]
        public string PostalCode { get; set; }

        [Display(Name = "Morada")]
        public string Address1 { get; set; }

        [Display(Name = "Porta")]
        public string Door { get; set; }

        [Display(Name = "Localidade")]
        public string Locality { get; set; }


        public long IdDistrict { get; set; }

         [Display(Name = "Districto")]
        public District IdDistrictNavigation { get; set; }
        public ICollection<Company> Company { get; set; }
        public ICollection<Student> Student { get; set; }
    }
}
