using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    public enum TypeMedia { Icon, PrimaryImage, Images }
    
    public class Media 
    {
        public Int64 Id { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public String Image { get; set; }

        public TypeMedia TypeMedia { get; set; }

        public Int64 ProjectId { get; set; }
        public Project Project { get; set; }

        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }

    }
}
