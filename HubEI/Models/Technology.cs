using System;
using System.Collections.Generic;

namespace HubEI.Models
{
    public partial class Technology
    {
        public Technology()
        {
            ProjectTechnology = new HashSet<ProjectTechnology>();
        }

        public long IdTechnology { get; set; }
        public string Description { get; set; }

        public ICollection<ProjectTechnology> ProjectTechnology { get; set; }
    }
}
