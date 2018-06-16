using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HubEI.Models
{
    public partial class BusinessArea
    {
        public BusinessArea()
        {
            Project = new HashSet<Project>();
        }

        public long IdBusinessArea { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<Project> Project { get; set; }
    }
}
