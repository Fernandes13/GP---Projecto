using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HubEI.Models
{
    public partial class ProjectType
    {
        public ProjectType()
        {
            Project = new HashSet<Project>();
        }

        public long IdProjectType { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<Project> Project { get; set; }
    }
}
