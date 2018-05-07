using System;
using System.Collections.Generic;

namespace HubEI.Models
{
    public partial class ProjectTechnology
    {
        public long IdProject { get; set; }
        public long IdTechnology { get; set; }

        public Project IdProjectNavigation { get; set; }
        public Technology IdTechnologyNavigation { get; set; }
    }
}
