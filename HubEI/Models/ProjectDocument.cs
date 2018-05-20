using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HubEI.Models
{
    public partial class ProjectDocument
    {
        public long IdProjectDocument { get; set; }
        public byte[] Document { get; set; }
        public long IdProject { get; set; }

        [Display(Name = "Nome")]
        public string FileName { get; set; }

        [Display(Name = "Tamanho")]
        public double FileSize { get; set; }

        public Project IdProjectNavigation { get; set; }
    }
}
