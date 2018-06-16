using System;
using System.Collections.Generic;

namespace HubEI.Models
{
    public partial class District
    {
        public District()
        {
            Address = new HashSet<Address>();
        }

        public long IdDistrict { get; set; }
        public string Description { get; set; }

        public ICollection<Address> Address { get; set; }

        public ICollection<Company> Company { get; set; }
    }
}
