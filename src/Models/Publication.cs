using System;
using System.Collections.Generic;

namespace Models
{
    public class Publication: BaseEntity<Int16>  
    {
        public ICollection<ProjectPublication> Projects { get; set; }

    }
}
