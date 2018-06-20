using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubEI.Models.ViewModels
{
    /// <summary>
    ///  Class used to provide project and mentor models to the View.
    /// </summary>
    public class ProjectMentorViewModel
    {
        public SchoolMentor Mentor;

        public double AverageGradeGiven;

        public ICollection<Project> Projects;
    }
}
