using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubEI.Models.ViewModels
{
    public class ProjectMentorViewModel
    {
        public SchoolMentor Mentor;

        public double AverageGradeGiven;

        public ICollection<Project> Projects;
    }
}
