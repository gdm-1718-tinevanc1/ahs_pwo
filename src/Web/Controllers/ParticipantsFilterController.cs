using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Database;
using Models;
using Models.Security;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class ParticipantsFilterController : BaseController
    {
        private const string FAILGETENTITIES = "Failed to get Participant from the API";
        private const string FAILGETENTITYBYID = "Failed to get Participant from the API by Id: {0}";

        public ParticipantsFilterController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }


        [HttpGet("{typeParticipants}", Name = "GetParticipantsByType")]
        public async Task<IActionResult> GetParticipantsByType(String typeParticipants)
        {
            // TypeParticipant type;
            var model = await ApplicationDbContext.Participants.Where(t => t.TypeParticipant == (TypeParticipant) Enum.Parse(typeof(TypeParticipant), typeParticipants)).ToListAsync();

           
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITIES);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }
    }
}
