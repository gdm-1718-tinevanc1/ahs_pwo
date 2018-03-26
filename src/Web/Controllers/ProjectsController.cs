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
    public class ProjectsController : BaseController
    {
        private const string FAILGETENTITIES = "Failed to get Project from the API";
        private const string FAILGETENTITYBYID = "Failed to get Project from the API by Id: {0}";
    
        public ProjectsController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }

        // GET api/projects
        [HttpGet(Name = "GetProjects")]
        public async Task<IActionResult> GetProjects([FromQuery] Nullable<bool> withChildren)
        {
            var model = (withChildren != null && withChildren == true)?await ApplicationDbContext.Projects.Include(f => f.Status).Include(f => f.Financingforms).ThenInclude(f => f.Financingform)
            .Include(m => m.Mediums).Include(m => m.Links).Include(p => p.Partners).Include(m => m.Profiles).ThenInclude(p => p.Profile).Include(p => p.Budget).Include(t => t.Tags).Include(p => p.Publications)
            .Include(p => p.Participants).ThenInclude(p => p.Participant).OrderByDescending(o => o.CreatedAt).ToListAsync():await ApplicationDbContext.Projects.OrderByDescending(o => o.CreatedAt).ToListAsync();

           
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITIES);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }

        [HttpGet("{projectId:int}", Name = "GetprojectById")]
        public async Task<IActionResult> GetProjectById(Int16 projectId)
        {
            var model = await ApplicationDbContext.Projects.Include(f => f.Status).Include(f => f.Financingforms).ThenInclude(f => f.Financingform)
            .Include(m => m.Mediums).Include(m => m.Links).Include(p => p.Partners).Include(m => m.Profiles).ThenInclude(p => p.Profile).Include(p => p.Budget).Include(t => t.Tags).Include(p => p.Publications)
            .Include(p => p.Participants).ThenInclude(p => p.Participant).FirstOrDefaultAsync(o => o.Id == projectId);
            
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, projectId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }


        [HttpPost(Name = "CreateProject")]
        public async Task<IActionResult> CreateProject([FromBody] Project item)
       {
            var objectValue = "{\"nl\": { \"value\": \"\", \"validate\": {\"allow_changes\":  false, \"required\": false, \"publication_ok\": false, \"validated_by\": null, \"feedback\": null, \"visible_to\": [] }}, \"en\": { \"value\": null, \"validate\": {\"allow_changes\":  false, \"required\": false, \"publication_ok\": false, \"validated_by\": null, \"feedback\": null, \"visible_to\": [] }}}";
            // objectValue = JSON.stringify(objectValue);
            // var objectValidate ="\"{ \\\"validate\\\": {\\\"allow_changes\\\":  false, \\\"required\\\": false, \\\"publication_ok\\\": false, \\\"validated_by\\\": null, \\\"feedback\\\": null, \\\"visible_to\\\": [] }}\"";

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

            if(item.Title == null && item.Shorttitle == null && item.Subtitle == null && item.Description == null && item.Startdate == null && item.Enddate == null){
                item.Title = objectValue;
                item.Shorttitle = objectValue;
                item.Subtitle = objectValue;
                item.Description = objectValue;                
            }

            if(item.Abstract == null){
                item.Abstract = objectValue;              
            }


           /*      var oldProjectProfiles = ApplicationDbContext.ProjectProfile.Where(m => m.ProjectId == item.Id).ToList(); 

                foreach(var oldProjectProfile in oldProjectProfiles) {
                    ApplicationDbContext.Entry(oldProjectProfile).State = EntityState.Deleted;
                    ApplicationDbContext.SaveChanges();
                }  
            
                if(item.Profiles != null){
                    foreach (var profile in item.Profiles.ToList())
                    {
                        Profile profile1 = ApplicationDbContext.Profiles.FirstOrDefault(m => m.Id == profile.ProfileId);

                        var af = new ProjectProfile { Project = item, Profile = profile1 };

                        ApplicationDbContext.ProjectProfile.Add(af);        
                    }
                }
 */


            ApplicationDbContext.Projects.Add(item);
            await ApplicationDbContext.SaveChangesAsync();

           return this.CreatedAtRoute("GetProjectById", new { Controller = "ProjectsController", projectId = item.Id }, item);
       }

        [HttpPut("{projectId:int}", Name = "UpdateProject")]
        public async Task<IActionResult> UpdateProject(Int16 ProjectId, [FromBody] Project item)
        {
            if(item == null || item.Id != ProjectId)
            {
                return BadRequest();
            }

            var model = await ApplicationDbContext.Projects.Include(f => f.Financingforms).Include(p => p.Participants).ThenInclude(p => p.Participant).FirstOrDefaultAsync(o => o.Id == ProjectId);

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
            } 

            if(item.Abstract != null) model.Abstract = item.Abstract;

            // budget
            if(model.Budget != null){
                Budget budget;
                if (model.BudgetId > 0)
                    budget = ApplicationDbContext.Budgets.FirstOrDefault(m => m.Id == model.BudgetId);
                else
                {
                    budget = new Budget();
                }

                budget.TotalBudget = model.Budget.TotalBudget;
                budget.ArteveldeBudget = model.Budget.ArteveldeBudget;
                budget.InvestmentBudget = model.Budget.InvestmentBudget;
                budget.OperatingBudget = model.Budget.OperatingBudget;
                budget.StaffBudget = model.Budget.StaffBudget;
                model.BudgetValidate = item.BudgetValidate;

                model.Budget = budget;
            }


            
            if(item.Financingforms != null || item.Participants != null || item.Tags != null ||item.Partners != null || item.Links != null ){
                // many financingform; delete old + add new
                var oldProjectFinancingforms = ApplicationDbContext.ProjectFinancingform.Where(m => m.ProjectId == model.Id).ToList();            
            
                foreach(var oldProjectFinancingform in oldProjectFinancingforms) {
                    //model.Financingforms.Remove(oldProjectFinancingform);  
                    ApplicationDbContext.Entry(oldProjectFinancingform).State = EntityState.Deleted;
                    ApplicationDbContext.SaveChanges();
                }   
                if(item.Financingforms != null){
                    foreach (var financingform in item.Financingforms.ToList())
                    {
                        Financingform financingform1 = ApplicationDbContext.Financingforms.FirstOrDefault(m => m.Id == financingform.FinancingformId);

                        var af = new ProjectFinancingform { Project = item, Financingform = financingform1 };

                        ApplicationDbContext.ProjectFinancingform.Add(af);
                    }
                }
        

                // many participants; delete old + add new
                var oldProjectParticipants = ApplicationDbContext.ProjectParticipant.Where(m => m.ProjectId == model.Id).ToList(); 

                foreach(var oldProjectParticipant in oldProjectParticipants) {
                    ApplicationDbContext.Entry(oldProjectParticipant).State = EntityState.Deleted;
                    ApplicationDbContext.SaveChanges();
                }  
            
                if(item.Participants != null){
                    foreach (var participant in item.Participants.ToList())
                    {
                        Participant participant1 = ApplicationDbContext.Participants.FirstOrDefault(m => m.Id == participant.ParticipantId);

                        var af = new ProjectParticipant { Project = item, Participant = participant1 };

                        ApplicationDbContext.ProjectParticipant.Add(af);        
                    }
                }
    


                //tags
                var oldTags = ApplicationDbContext.Tags.Where(m => m.ProjectId == model.Id).ToList(); 

                foreach(var oldTag in oldTags) {
                    ApplicationDbContext.Entry(oldTag).State = EntityState.Deleted;
                    ApplicationDbContext.SaveChanges();
                }   
            
                List<Tag> tags = new List<Tag>();
                if(item.Tags != null){
                    foreach (var tag in item.Tags.ToList())
                    {
                        var tag1 = new Tag();
                        tag1.Name = tag.Name;
                                
                        tags.Add(tag1);                
                        model.Tags = tags;
                    }
                }


                //partners
                var oldPartners = ApplicationDbContext.Partners.Where(m => m.ProjectId == model.Id).ToList(); 
                foreach(var oldPartner in oldPartners) {
                    ApplicationDbContext.Entry(oldPartner).State = EntityState.Deleted;
                    ApplicationDbContext.SaveChanges();
                }  

                List<Partner> partners = new List<Partner>();
                if(item.Partners != null){
                    foreach (var partner in item.Partners.ToList())
                    {
                        var partner1 = new Partner();
                        partner1.Name = partner.Name;
                            
                        partners.Add(partner1);                
                        model.Partners = partners;
                    } 
                }


                //links
                var oldLinks = ApplicationDbContext.Links.Where(m => m.ProjectId == model.Id).ToList(); 

                foreach(var oldLink in oldLinks) {
                    ApplicationDbContext.Entry(oldLink).State = EntityState.Deleted;
                    ApplicationDbContext.SaveChanges();
                }  
            
                List<Link> links = new List<Link>();
                if(item.Links != null){
                    foreach (var link in item.Links.ToList())
                    {
                        var link1 = new Link();
                        link1.Name = link.Name;
                            
                        links.Add(link1);                
                        model.Links = links;
                    } 
                }
           }

           //mediums
           if(item.Mediums != null){
                var oldMediums = ApplicationDbContext.Mediums.Where(m => m.ProjectId == model.Id).ToList(); 

                foreach(var oldMedia in oldMediums) {
                    ApplicationDbContext.Entry(oldMedia).State = EntityState.Deleted;
                    ApplicationDbContext.SaveChanges();
                }  
            
                List<Media> mediums = new List<Media>();
                foreach (var media in item.Mediums.ToList())
                {
                    var media1 = new Media();
                    media1.Image = media.Image;
                    media1.TypeMedia = media.TypeMedia;
                            
                    mediums.Add(media1);                
                    model.Mediums = mediums;
                } 
           }

            
            ApplicationDbContext.Projects.Attach(model);
            ApplicationDbContext.Entry(model).State = EntityState.Modified;
            await ApplicationDbContext.SaveChangesAsync();

            return new NoContentResult();
        }

    }
}
