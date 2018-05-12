using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubEI.Models.ViewModels
{
    public class BOMentorViewModel
    {
        public SchoolMentor Mentor { get; set; }

        public IEnumerable<SchoolMentor> Mentors { get; set; }
    }
}
