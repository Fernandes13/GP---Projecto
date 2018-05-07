using System;
using System.Collections.Generic;

namespace HubEI.Models
{
    public partial class SchoolAdvisor
    {
        public SchoolAdvisor()
        {
            ProjectAdvisor = new HashSet<ProjectAdvisor>();
        }

        public long IdSchoolAdvisor { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<ProjectAdvisor> ProjectAdvisor { get; set; }
    }
}
