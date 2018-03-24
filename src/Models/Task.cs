using System;
using System.Collections.Generic;

namespace Models
{
    public class Task: BaseEntity<Int16> 
    {
        public Int32 ProfileId { get; set; }
        public Profile Profile { get; set; }

        public Int64 ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
