using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HubEI.Models.ViewModels
{
    public class BOStudentViewModel
    {
        public Student Student { get; set; }
        public Address Address { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<SelectListItem> Branches { get; set; }
        public IEnumerable<SelectListItem> Districts { get; set; }
    }
}

