using System;
using System.Collections.Generic;

namespace Models
{
    public class ProjectSpearhead 
    {
        public Int16 SpearheadId { get; set; }
        public Spearhead Spearhead { get; set; }

        public Int64 ProjectId { get; set; }
        public Project Project { get; set; }

    }
}
