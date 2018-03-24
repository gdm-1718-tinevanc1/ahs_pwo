using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNet.WebApi.Cors;

using Database;
using Models;
using Models.Security;


namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : BaseController
    {
        private const string FAILGETENTITIES = "Failed to get Task from the API";
        private const string FAILGETENTITYBYID = "Failed to get Task from the API by Id: {0}";

        public TasksController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }

        // GET api/tasks
        [HttpGet(Name = "GetTasks")]
        public async Task<IActionResult> GetTasks([FromQuery] Nullable<bool> withChildren)
        {
            // var model = await ApplicationDbContext.Locations.Include(c => c.City.Country).Include(a => a.Activities).ToListAsync();

            var model = await ApplicationDbContext.Tasks.Include(p => p.Project).OrderByDescending(o => o.CreatedAt).Where(o => o.Project.StatusId == 3).ToListAsync();

            if (model == null)
            {
                var msg = String.Format(FAILGETENTITIES);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }

        [HttpGet("{profileId:int}", Name = "GetTaskByProfileId")]
        public async Task<IActionResult> GetTaskByProfileId(Int32 profileId)
        {
            var model = await ApplicationDbContext.Tasks.Where(o => o.ProfileId == profileId).ToListAsync();

            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, profileId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }


        [HttpPost(Name = "CreateTask")]
        // [EnableCors("AllowAll")]
        public async Task<IActionResult> CreateTask([FromBody] Models.Task item)
       {
            if(item == null)
            {
               return BadRequest();
            }

            ApplicationDbContext.Tasks.Add(item);
            await ApplicationDbContext.SaveChangesAsync();

           return this.CreatedAtRoute("GetTaskByProfileId", new { Controller = "TasksController", profileId = item.ProfileId }, item);
       }

    }
}
