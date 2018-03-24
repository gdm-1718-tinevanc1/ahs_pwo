using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Models
{
    public class Financingform: BaseEntity<Int16> 
    {
        // [JsonIgnore] 
        public ICollection<ProjectFinancingform> Projects { get; set; }

    }
}
