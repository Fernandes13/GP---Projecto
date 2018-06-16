using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HubEI.Models
{
    public partial class Company
    {
        public Company()
        {
            Project = new HashSet<Project>();
        }

        public long IdCompany { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "District")]
        public long IdDistrict { get; set; }

        [Display(Name = "District")]
        public District IdDistrictNavigation { get; set; }

        [JsonIgnore]
        public ICollection<Project> Project { get; set; }
    }
}
