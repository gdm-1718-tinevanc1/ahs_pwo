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
    public class ProjectstatusFilterController : BaseController
    {
        private const string FAILGETENTITIES = "Failed to get Project status filter from the API";
        private const string FAILGETENTITYBYID = "Failed to get Project status filter from the API by Id: {0}";

        public ProjectstatusFilterController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }


        [HttpGet("{typeProjectstatus:int}", Name = "GetProjectsByStatus")]
        public async Task<IActionResult> GetProjectsByStatus(Int16 typeProjectstatus)
        {
            var model = await ApplicationDbContext.Projects.Include(f => f.Status).Include(f => f.Financingforms).ThenInclude(f => f.Financingform)
            .Include(m => m.Mediums).Include(m => m.Links).Include(p => p.Partners).Include(m => m.Profiles).ThenInclude(p => p.Profile).Include(p => p.Budget).Include(t => t.Tags).Include(p => p.Publications)
            .Include(p => p.Participants).ThenInclude(p => p.Participant).Where(t => t.StatusId == typeProjectstatus).ToListAsync();
        
           
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITIES);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }
    }
}
