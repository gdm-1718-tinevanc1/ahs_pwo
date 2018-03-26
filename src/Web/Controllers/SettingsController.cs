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
    public class SettingsController : BaseController
    {
        private const string FAILGETENTITIES = "Failed to get Todo from the API";
        private const string FAILGETENTITYBYID = "Failed to get Todo from the API by Id: {0}";

        public SettingsController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }

        // GET api/profiles
        [HttpGet("{profileId:int}", Name = "GetSetting")]
        public async Task<IActionResult> GetSetting(Int16 profileId)
        {
             var model = await ApplicationDbContext.Profiles.Include(f => f.Setting).FirstOrDefaultAsync(o => o.Id == profileId);
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, profileId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }
/* 

        [HttpPost(Name = "CreateSetting")]
        public async Task<IActionResult> CreateSetting([FromBody] Models.Profile item)
       {
            if(item == null)
            {
               return BadRequest();
            }

            ApplicationDbContext.Profiles.Add(item);
            await ApplicationDbContext.SaveChangesAsync();

           return this.CreatedAtRoute("GetProfileById", new { Controller = "ProfilesController", profileId = item.Id }, item);
       } */


        [HttpPut("{profileId:int}", Name = "UpdateSettings")]
        public async Task<IActionResult> UpdateSettings(Int16 ProfileId, [FromBody] Profile item)
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

            Setting setting = new Setting();
            setting.Language = item.Setting.Language;
            setting.Color = item.Setting.Color;
        
            model.Setting = setting;


            ApplicationDbContext.Profiles.Attach(model);
            ApplicationDbContext.Entry(model).State = EntityState.Modified;
            await ApplicationDbContext.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}
