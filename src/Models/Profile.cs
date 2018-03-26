using System;
using System.Collections.Generic;

namespace Models
{
    public class Profile 
    {
        public Int32 Id { get; set; }
        
        public string UserName { get; set; }

        public string Email { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Image { get; set; }
        
        public Int32 Employeenumber { get; set; }

        public Nullable<Int32> SettingId { get; set; }
        public Setting Setting { get; set; }

        public List<Task> Tasks { get; set; }
        public List<ProjectProfile> Projects { get; set; }

        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }

    }
}




