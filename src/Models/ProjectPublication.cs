using System;
using System.Collections.Generic;

namespace Models
{
    public class ProjectPublication
    {
        public Int16 PublicationId { get; set; }
        public Publication Publication { get; set; }

        public Int64 ProjectId { get; set; }
        public Project Project { get; set; }

    }
}
