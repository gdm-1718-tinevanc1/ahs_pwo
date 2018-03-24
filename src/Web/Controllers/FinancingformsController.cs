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
    public class FinancingformsController : BaseController
    {
        private const string FAILGETENTITIES = "Failed to get Todo from the API";
        private const string FAILGETENTITYBYID = "Failed to get Todo from the API by Id: {0}";

        public FinancingformsController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }

        // GET api/financingforms
        [HttpGet(Name = "GetFinancingforms")]
        public async Task<IActionResult> GetFinancingforms([FromQuery] Nullable<bool> withChildren)
        {
            // var model = await ApplicationDbContext.Locations.Include(c => c.City.Country).Include(a => a.Activities).ToListAsync();

            var model = (withChildren != null && withChildren == true)?await ApplicationDbContext.Financingforms.ToListAsync():await ApplicationDbContext.Financingforms.ToListAsync();

           
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITIES);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }

        [HttpGet("{financingformId:int}", Name = "GetFinancingformById")]
        public async Task<IActionResult> GetFinancingformById(Int16 financingformId)
        {
            var model = await ApplicationDbContext.Financingforms.FirstOrDefaultAsync(o => o.Id == financingformId);
            
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, financingformId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }

       [HttpPost(Name = "CreateFinancingform")]
        // [EnableCors("AllowAll")]
        public async Task<IActionResult> CreateFinancingform([FromBody] Models.Financingform item)
       {
            if(item == null)
            {
               return BadRequest();
            }

            ApplicationDbContext.Financingforms.Add(item);
            await ApplicationDbContext.SaveChangesAsync();

           return this.CreatedAtRoute("GetFinancingformById", new { Controller = "FinancingformsController", financingformId = item.Id }, item);
       }

    }
}
