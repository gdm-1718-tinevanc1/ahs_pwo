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
    public class LinksController : BaseController
    {
        private const string FAILGETENTITIES = "Failed to get Todo from the API";
        private const string FAILGETENTITYBYID = "Failed to get Todo from the API by Id: {0}";

        public LinksController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }

        // GET api/links
        [HttpGet(Name = "GetLinks")]
        public async Task<IActionResult> GetLinks([FromQuery] Nullable<bool> withChildren)
        {
            // var model = await ApplicationDbContext.Locations.Include(c => c.City.Country).Include(a => a.Activities).ToListAsync();

            var model = (withChildren != null && withChildren == true)?await ApplicationDbContext.Links.ToListAsync():await ApplicationDbContext.Links.ToListAsync();

           
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITIES);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }

        [HttpGet("{linkId:int}", Name = "GetLinkById")]
        public async Task<IActionResult> GetLinkById(Int16 linkId)
        {
            var model = await ApplicationDbContext.Links.FirstOrDefaultAsync(o => o.Id == linkId);

            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, linkId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }
    }
}
