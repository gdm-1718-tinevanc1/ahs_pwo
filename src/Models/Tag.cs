using System;
using System.Collections.Generic;

namespace Models
{
    public class Tag: BaseEntity<Int16> 
    {
        // public string Description { get; set; }

        //public List<ProjectTag> Projects { get; set; }

        public Int64 ProjectId { get; set; }
        public Project Project { get; set; }

    }
}
