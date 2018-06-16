using HubEI.Models.CustomValidations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HubEI.Models.ViewModels
{
    public class BOCompanyViewModel
    {

        public Company Company { get; set; }

        public IEnumerable<Company> Companies { get; set; }

        //[Display(Name = "Docentes Orientadores")]
        //public List<MentorsCheckBox> Mentors { get; set; }
    }
}