using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubEI.Models.ViewModels
{
    /// <summary>
    ///  Class used to provide mentor model to the View.
    /// </summary>
    public class BOMentorViewModel
    {
        public SchoolMentor Mentor { get; set; }

        public IEnumerable<SchoolMentor> Mentors { get; set; }
    }
}
