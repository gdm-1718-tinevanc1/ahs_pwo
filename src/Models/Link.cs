using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Models
{
    public class Link: BaseEntity<Int16> 
    {
        public Int64 ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
