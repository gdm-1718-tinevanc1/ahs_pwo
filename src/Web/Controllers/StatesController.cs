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
    public class StatesController : BaseController
    {
        private const string FAILGETENTITIES = "Failed to get Todo from the API";
        private const string FAILGETENTITYBYID = "Failed to get Todo from the API by Id: {0}";

        public StatesController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }

        // GET api/states
        [HttpGet(Name = "GetStates")]
        public async Task<IActionResult> GetStates([FromQuery] Nullable<bool> withChildren)
        {
            var model = (withChildren != null && withChildren == true)?await ApplicationDbContext.States.ToListAsync():await ApplicationDbContext.States.ToListAsync();

            if (model == null)
            {
                var msg = String.Format(FAILGETENTITIES);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }
    }
}
