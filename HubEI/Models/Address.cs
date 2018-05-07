using System;
using System.Collections.Generic;

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
        public string PostalCode { get; set; }
        public string Address1 { get; set; }
        public string Door { get; set; }
        public string Locality { get; set; }
        public long IdDistrict { get; set; }

        public District IdDistrictNavigation { get; set; }
        public ICollection<Company> Company { get; set; }
        public ICollection<Student> Student { get; set; }
    }
}
