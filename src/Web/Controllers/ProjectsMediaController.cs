using System;
using System.IO; 

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNet.WebApi.Cors;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

using Database;
using Models;
using Models.Security;


namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsMediaController : BaseController
    {
        private const string FAILGETENTITIES = "Failed to get Project from the API";
        private const string FAILGETENTITYBYID = "Failed to get Project from the API by Id: {0}";
        private IHostingEnvironment _environment;

        public ProjectsMediaController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, IHostingEnvironment environment):base(applicationDbContext, userManager) 
        {
            _environment = environment;
        }

/*         // GET api/projects
        [HttpGet(Name = "GetProjects")]
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

        [HttpGet("{projectId:int}", Name = "GetProjectMediaById")]
        public async Task<IActionResult> GetProjectMediaById(Int16 projectId)
        {
            var model = await ApplicationDbContext.Projects.Include(m => m.Mediums).FirstOrDefaultAsync(o => o.Id == projectId);
            
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, projectId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }


        [HttpPut("{ProjectId:int}", Name = "UpdateProjectMedia")]
        public async Task<IActionResult> UpdateProjectMedia(Int16 ProjectId, [FromBody] Project item)
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

           //mediums
           if(item.Mediums != null){

            

                var oldMediums = ApplicationDbContext.Mediums.Where(m => m.ProjectId == model.Id).ToList(); 

                foreach(var oldMedia in oldMediums) {
                    ApplicationDbContext.Entry(oldMedia).State = EntityState.Deleted;
                    ApplicationDbContext.SaveChanges();
                }  
            
                
                int i = -1;
                List<Media> mediums = new List<Media>();
                foreach (var media in item.Mediums.ToList())
                {       
                    i++;
                    var media1 = new Media();

                    if (media.Image.StartsWith("assets/uploads")) 
                    {
                        media1.Image = media.Image;
                    }
                    else{

                        var base64string = media.Image;
                        string converted = base64string.Replace("data:image/png;base64,", String.Empty);
                        converted = converted.Replace("data:image/jpeg;base64,", String.Empty);
                        var base64array = Convert.FromBase64String(converted);
                            // var path = Path.Combine("~\\ClientApp\\app\\assets\\images\\", fileForController.FileName);
                        // var uploadPath = Path.Combine(_environment.ContentRootPath, "ClientApp\\src\\assets\\images\\");
                        var uploadPath = Path.Combine("ClientApp", "src", "assets", "uploads");

                        Directory.CreateDirectory(Path.Combine(uploadPath, ProjectId.ToString()));
                        System.IO.File.WriteAllBytes(Path.Combine(uploadPath, ProjectId.ToString(), media.TypeMedia + "_" + i + ".jpg"), base64array);
                        media1.Image = Path.Combine("assets", "uploads", ProjectId.ToString(), media.TypeMedia + "_" + i + ".jpg").ToString();
                    }

                    /* var media1 = new Media();
                    media1.Image = Path.Combine("assets", "uploads", ProjectId.ToString(), media.TypeMedia + "_" +  ProjectId.ToString() +".jpg").ToString();
                   */  
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
