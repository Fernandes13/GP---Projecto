using System;
using System.Collections.Generic;

namespace HubEI.Models
{
    public partial class ProjectAdvisor
    {
        public long IdProject { get; set; }
        public long IdSchoolMentor { get; set; }

        public Project IdProjectNavigation { get; set; }
        public SchoolMentor IdSchoolMentorNavigation { get; set; }
    }
}
