using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Models
{
    public class ProjectProfile
    {
        public Int32 ProfileId { get; set; }
        public Profile Profile { get; set; }

        public Int64 ProjectId { get; set; }
        [JsonIgnore] 
        public Project Project { get; set; }

    }
}

