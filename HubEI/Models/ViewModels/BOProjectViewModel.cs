﻿using HubEI.Models.CustomValidations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HubEI.Models.ViewModels
{
    /// <summary>
    ///  Class used to provide project related models to the View.
    /// </summary>
    /// 
    public class BOProjectViewModel
    {

        public Project Project { get; set; }

        [Required(ErrorMessage = "É necessário introduzir o relatório do projecto!")]
        [Display(Name = "Relatório")]
        [IsFilePDF]
        public IFormFile Report { get; set; }

        public IEnumerable<Project> Projects { get; set; }
        public IEnumerable<SelectListItem> Companies { get; set; }
        public IEnumerable<SelectListItem> Students { get; set; }
        public IEnumerable<SelectListItem> ProjectTypes { get; set; }

        [Display(Name = "Docentes Orientadores")]
        public List<MentorsCheckBox> Mentors { get; set; }

        [Display(Name = "Anexos")]
        public List<IFormFile> Attachments { get; set; }

        [Display(Name = "Company Email for Privacy Notification")]
        public String CompanyEmail { get; set; }
    }
}