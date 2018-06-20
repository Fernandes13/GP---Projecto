using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubEI.Models.ViewModels
{
    public class CompanyViewModel
    {
        public Company Company{get;set;}
        public ICollection<Project> Projects { get; set; }

        public ICollection<BusinessArea> BusinessAreas { get; set; }
    }
}
