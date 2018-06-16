﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HubEI.Models
{
    public partial class Address
    {
        public Address()
        {
            Student = new HashSet<Student>();
        }

        public long IdAddress { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Address")]
        public string Address1 { get; set; }

        [Display(Name = "Door")]
        public string Door { get; set; }

        [Display(Name = "Locality")]
        public string Locality { get; set; }


        public long IdDistrict { get; set; }

         [Display(Name = "District")]
        public District IdDistrictNavigation { get; set; }
        

        [JsonIgnore] 
        public ICollection<Student> Student { get; set; }
    }
}
