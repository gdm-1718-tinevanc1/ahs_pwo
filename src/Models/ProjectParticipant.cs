using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Models
{
    public class ProjectParticipant 
    {
        public Int16 ParticipantId { get; set; }
        public Participant Participant { get; set; }

        public Int64 ProjectId { get; set; }
        [JsonIgnore] 
        public Project Project { get; set; }
    }
}
