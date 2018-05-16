using System;
using System.Collections.Generic;

namespace HubEI.Models
{
    public partial class Admin
    {
        public long IdAdmin { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
    }
}
