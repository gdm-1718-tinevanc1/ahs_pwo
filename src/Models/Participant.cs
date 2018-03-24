using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;

namespace Models
{
    public enum TypeParticipant { ODC, Projectmedewerker, Opleiding }

    public class Participant: BaseEntity<Int16>  
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeParticipant TypeParticipant { get; set; }

        public ICollection<ProjectParticipant> Projects { get; set; }

    }
}
