using System;
using System.Collections.Generic;

namespace Models
{
    public class Tag: BaseEntity<Int16> 
    {
        public Int64 ProjectId { get; set; }
        public Project Project { get; set; }

    }
}
