using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HubEI.Models
{
    public partial class SchoolMentor
    {
        public SchoolMentor()
        {
            ProjectAdvisor = new HashSet<ProjectAdvisor>();
        }

        public long IdSchoolMentor { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Field has to be an email")]
        public string Email { get; set; }

        [JsonIgnore]
        public ICollection<ProjectAdvisor> ProjectAdvisor { get; set; }
    }
}
