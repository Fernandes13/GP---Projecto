using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HubEI.Models
{
    public partial class Company
    {
        public Company()
        {
            Project = new HashSet<Project>();
        }

        public long IdCompany { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long IdAddress { get; set; }

        public Address IdAddressNavigation { get; set; }

        [JsonIgnore]
        public ICollection<Project> Project { get; set; }
    }
}
