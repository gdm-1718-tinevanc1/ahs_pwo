using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNet.WebApi.Cors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

using Database;
using Models;
using Models.Security;


namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsGeneralController : BaseController
    {
        private const string FAILGETENTITIES = "Failed to get Project from the API";
        private const string FAILGETENTITYBYID = "Failed to get Project from the API by Id: {0}";

        public ProjectsGeneralController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }

        // GET api/projects
      /*   [HttpGet(Name = "GetProjects")]
        public async Task<IActionResult> GetProjects([FromQuery] Nullable<bool> withChildren)
        {
            var model = (withChildren != null && withChildren == true)?await ApplicationDbContext.Projects.Include(f => f.Financingforms).ThenInclude(f => f.Financingform)
            .Include(m => m.Mediums).Include(m => m.Links).Include(p => p.Partners).Include(m => m.Profiles).ThenInclude(p => p.Profile).Include(p => p.Budget).Include(t => t.Tags).Include(p => p.Publications)
            .Include(p => p.Participants).ThenInclude(p => p.Participant).OrderByDescending(o => o.CreatedAt).ToListAsync():await ApplicationDbContext.Projects.OrderByDescending(o => o.CreatedAt).ToListAsync();

           
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITIES);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        } */

        [HttpGet("{projectId:int}", Name = "GetProjectGeneralById")]
        public async Task<IActionResult> GetProjectGeneralById(Int16 projectId)
        {
            var model = await ApplicationDbContext.Projects.FirstOrDefaultAsync(o => o.Id == projectId);
            
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, projectId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }


/*         [HttpPost(Name = "CreateProject")]
        public async Task<IActionResult> CreateProject([FromBody] Project item)
       {
            if(item == null)
            {
               return BadRequest();
            }

            if(item.Budget != null){
                Budget budget = new Budget();
                budget.TotalBudget = item.Budget.TotalBudget;
                budget.ArteveldeBudget = item.Budget.ArteveldeBudget;
                budget.InvestmentBudget = item.Budget.InvestmentBudget;
                budget.OperatingBudget = item.Budget.OperatingBudget;
                budget.StaffBudget = item.Budget.StaffBudget;

                item.Budget = budget;
            }


            ApplicationDbContext.Projects.Add(item);
            await ApplicationDbContext.SaveChangesAsync();

           return this.CreatedAtRoute("GetProjectById", new { Controller = "ProjectsController", projectId = item.Id }, item);
       } */

        [HttpPut("{projectId:int}", Name = "UpdateProjectGeneral")]
        public async Task<IActionResult> UpdateProjectGeneral(Int16 ProjectId, [FromBody] Project item)
        {
            if(item == null || item.Id != ProjectId)
            {
                return BadRequest();
            }

            var model = await ApplicationDbContext.Projects.FirstOrDefaultAsync(o => o.Id == ProjectId);

            if(model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, ProjectId);
                return NotFound(msg);
            }


            if(item.Title != null) {
                model.Title = item.Title;
                model.Shorttitle = item.Shorttitle;
                model.Subtitle = item.Subtitle;
                model.Description = item.Description;
                model.Startdate = item.Startdate;
                model.Enddate = item.Enddate;
                model.StatusId = item.StatusId;
            }

            if(item.Abstract != null) model.Abstract = item.Abstract;

            
            ApplicationDbContext.Projects.Attach(model);
            ApplicationDbContext.Entry(model).State = EntityState.Modified;
            await ApplicationDbContext.SaveChangesAsync();

            return new NoContentResult();
        }

    }
}
