using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HubEI.Models
{
    [Table("Business_area")]
    public class BusinessArea
    {
        public BusinessArea()
        {
            Project = new HashSet<Project>();
        }

        public long IdBusinessArea { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<Project> Project { get; set; }
    }
}
