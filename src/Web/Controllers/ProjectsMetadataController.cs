using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

using Database;
using Models;
using Models.Security;


namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsMetadataController : BaseController
    {
        private const string FAILGETENTITIES = "Failed to get Project from the API";
        private const string FAILGETENTITYBYID = "Failed to get Project from the API by Id: {0}";

        public ProjectsMetadataController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }

        // GET api/projects
       /* [HttpGet(Name = "GetProjects")]
        public async Task<IActionResult> GetProjectsMetadata([FromQuery] Nullable<bool> withChildren)
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

        [HttpGet("{projectId:int}", Name = "GetProjectMetadataById")]
        public async Task<IActionResult> GetProjectMetadataById(Int16 projectId)
        {
            var model = await ApplicationDbContext.Projects.Include(f => f.Financingforms).ThenInclude(f => f.Financingform)
            .Include(m => m.Links).Include(p => p.Partners).Include(t => t.Tags)
            .Include(p => p.Participants).ThenInclude(p => p.Participant).FirstOrDefaultAsync(o => o.Id == projectId);
            
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, projectId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }

/* 
        [HttpPost(Name = "CreateProject")]
        public async Task<IActionResult> CreateProjectMetadata([FromBody] Project item)
       {
            if(item == null)
            {
               return BadRequest();
            }

            ApplicationDbContext.Projects.Add(item);
            await ApplicationDbContext.SaveChangesAsync();

           return this.CreatedAtRoute("GetProjectById", new { Controller = "ProjectsController", projectId = item.Id }, item);
       }
 */
        [HttpPut("{projectId:int}", Name = "UpdateProjectMetadata")]
        public async Task<IActionResult> UpdateProjectMetadata(Int16 ProjectId, [FromBody] Project item)
        {
            if(item == null || item.Id != ProjectId)
            {
                return BadRequest();
            }

            var model = await ApplicationDbContext.Projects.Include(f => f.Financingforms)
            .Include(p => p.Participants).ThenInclude(p => p.Participant).FirstOrDefaultAsync(o => o.Id == ProjectId);

            if(model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, ProjectId);
                return NotFound(msg);
            }

                model.PartnerValidate = item.PartnerValidate;
                model.ParticipantValidate = item.ParticipantValidate;
                model.FinancingformValidate = item.FinancingformValidate;
                model.LinkValidate = item.LinkValidate;
    
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


            
            ApplicationDbContext.Projects.Attach(model);
            ApplicationDbContext.Entry(model).State = EntityState.Modified;
            await ApplicationDbContext.SaveChangesAsync();

            return new NoContentResult();
        }

    }
}
