using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubEI.Models.ViewModels
{
    public class CompaniesListViewModel
    {

        public Company Company { get; set; }

        public ICollection<BusinessArea> BusinessAreas { get; set; }

        public ICollection<Project> Projects { get; set; }


    }
}
