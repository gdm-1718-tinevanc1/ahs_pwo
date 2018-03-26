using System;
using System.Collections.Generic;

namespace Models
{
    public class Setting 
    {
        public Int32 Id { get; set; }
        
        public string Language { get; set; }
        public string Color { get; set; }
        
        public List<Profile> Profile { get; set; }

        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }

    }
}




