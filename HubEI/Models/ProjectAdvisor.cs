using System;
using System.Collections.Generic;

namespace HubEI.Models
{
    public partial class ProjectAdvisor
    {
        public long IdProject { get; set; }
        public long IdSchoolAdvisor { get; set; }

        public Project IdProjectNavigation { get; set; }
        public SchoolAdvisor IdSchoolAdvisorNavigation { get; set; }
    }
}
