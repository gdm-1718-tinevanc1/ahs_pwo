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
    public class ParticipantsController : BaseController
    {
        private const string FAILGETENTITIES = "Failed to get Participant from the API";
        private const string FAILGETENTITYBYID = "Failed to get Participant from the API by Id: {0}";

        public ParticipantsController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }

        // GET api/participants
        [HttpGet(Name = "GetParticipants")]
        public async Task<IActionResult> GetParticipants([FromQuery] Nullable<bool> withChildren)
        {
            // var model = await ApplicationDbContext.Locations.Include(c => c.City.Country).Include(a => a.Activities).ToListAsync();

            var model = (withChildren != null && withChildren == true)?await ApplicationDbContext.Participants.ToListAsync():await ApplicationDbContext.Participants.ToListAsync();

           
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITIES);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }


        [HttpGet("{participantId:int}", Name = "GetParticipantById")]
        public async Task<IActionResult> GetParticipantById(Int16 participantId)
        {
            var model = await ApplicationDbContext.Participants.FirstOrDefaultAsync(o => o.Id == participantId);
            
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, participantId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        } 
    }
}
