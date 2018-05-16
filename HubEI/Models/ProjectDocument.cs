using System;
using System.Collections.Generic;

namespace HubEI.Models
{
    public partial class ProjectDocument
    {
        public long IdProjectDocument { get; set; }
        public byte[] Document { get; set; }
        public long IdProject { get; set; }

        public Project IdProjectNavigation { get; set; }
    }
}
