using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;

namespace Models
{

    public class Partner: BaseEntity<Int16>  
    {
        public Int64 ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
