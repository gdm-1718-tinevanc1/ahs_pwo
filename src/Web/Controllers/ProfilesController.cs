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
    public class ProfilesController : BaseController
    {
        private const string FAILGETENTITIES = "Failed to get Todo from the API";
        private const string FAILGETENTITYBYID = "Failed to get Todo from the API by Id: {0}";

        public ProfilesController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }

        // GET api/profiles
        [HttpGet(Name = "GetProfiles")]
        public async Task<IActionResult> GetProfiles([FromQuery] Nullable<bool> withChildren)
        {
            var model = (withChildren != null && withChildren == true)?await ApplicationDbContext.Profiles.OrderByDescending(o => o.CreatedAt).ToListAsync():await ApplicationDbContext.Profiles.OrderByDescending(o => o.CreatedAt).ToListAsync();

            /*var model = (withChildren != null && withChildren == true)?await ApplicationDbContext.Profiles.Include(l => l.Location.City.Country)
            .Include(a => a.AccomodationType).Include(a => a.AccomodationPrice).Include(a => a.RentalType)
            .Include(a => a.Image).Include(a => a.Reviews).Include(a => a.Accessibilities)
            .Include(a => a.FoodIncludeds).Include(a => a.Services).Include(a => a.Rooms).ToListAsync():await ApplicationDbContext.Profiles.ToListAsync(); */

           
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITIES);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }

        [HttpGet("{profileId:int}", Name = "GetprofileById")]
        public async Task<IActionResult> GetProfileById(Int16 profileId)
        {
            var model = await ApplicationDbContext.Profiles.Include(s => s.Setting).FirstOrDefaultAsync(o => o.Id == profileId);
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, profileId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }


        [HttpPost(Name = "CreateProfile")]
        // [EnableCors("AllowAll")]
        public async Task<IActionResult> CreateProfile([FromBody] Models.Profile item)
       {
            if(item == null)
            {
               return BadRequest();
            }

            ApplicationDbContext.Profiles.Add(item);
            await ApplicationDbContext.SaveChangesAsync();

           return this.CreatedAtRoute("GetProfileById", new { Controller = "ProfilesController", profileId = item.Id }, item);
       }


        [HttpPut("{profileId:int}", Name = "UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(Int16 ProfileId, [FromBody] Profile item)
        {
            if(item == null || item.Id != ProfileId)
            {
                return BadRequest();
            }

            var model = await ApplicationDbContext.Profiles.FirstOrDefaultAsync(o => o.Id == ProfileId);

            if(model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, ProfileId);
                return NotFound(msg);
            }

            if (item.Image.StartsWith("assets/uploads") || item.Image.StartsWith("http://")) 
            {
                model.Image = item.Image;
            } 
            else {
                var base64string = item.Image;
                string converted = base64string.Replace("data:image/png;base64,", String.Empty);
                converted = converted.Replace("data:image/jpeg;base64,", String.Empty);
                var base64array = Convert.FromBase64String(converted);
                var uploadPath = Path.Combine("ClientApp", "src", "assets", "user_images");

                Directory.CreateDirectory(Path.Combine(uploadPath, model.Id.ToString()));
                System.IO.File.WriteAllBytes(Path.Combine(uploadPath, model.Id.ToString(), ".jpg"), base64array);
                model.Image = Path.Combine("assets", "user_images", model.Id.ToString(), ".jpg").ToString();
             };

            ApplicationDbContext.Profiles.Attach(model);
            ApplicationDbContext.Entry(model).State = EntityState.Modified;
            await ApplicationDbContext.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}
