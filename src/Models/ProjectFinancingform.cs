using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class ProjectFinancingform
    {
        public Int16 FinancingformId { get; set; }
        public Financingform Financingform { get; set; }

        public Int64 ProjectId { get; set; }
        [JsonIgnore] 
        public Project Project { get; set; }
    }
}


