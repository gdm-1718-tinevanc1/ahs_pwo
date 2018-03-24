using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Models
{
    public class Status: BaseEntity<Int64>  
    {
        [JsonIgnore] 
        public List<Project> Projects { get; set; }

    }
}
