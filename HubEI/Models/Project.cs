using System;
using System.Collections.Generic;

namespace HubEI.Models
{
    public partial class Project
    {
        public Project()
        {
            ProjectAdvisor = new HashSet<ProjectAdvisor>();
            ProjectDocument = new HashSet<ProjectDocument>();
            ProjectTechnology = new HashSet<ProjectTechnology>();
        }

        public long IdProject { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Report { get; set; }
        public DateTime ProjectDate { get; set; }
        public byte IsVisible { get; set; }
        public int Views { get; set; }
        public int Downloads { get; set; }
        public long IdProjectType { get; set; }
        public long IdStudent { get; set; }
        public long IdCompany { get; set; }

        public Company IdCompanyNavigation { get; set; }
        public ProjectType IdProjectTypeNavigation { get; set; }
        public Student IdStudentNavigation { get; set; }
        public ICollection<ProjectAdvisor> ProjectAdvisor { get; set; }
        public ICollection<ProjectDocument> ProjectDocument { get; set; }
        public ICollection<ProjectTechnology> ProjectTechnology { get; set; }
    }
}
