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
    public class BudgetController : BaseController
    {
        private const string FAILGETENTITIES = "Failed to get Todo from the API";
        private const string FAILGETENTITYBYID = "Failed to get Todo from the API by Id: {0}";

        public BudgetController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager):base(applicationDbContext, userManager) 
        {
        }

        // GET api/budget
        [HttpGet(Name = "GetBudgets")]
        public async Task<IActionResult> GetBudgets([FromQuery] Nullable<bool> withChildren)
        {
            // var model = await ApplicationDbContext.Locations.Include(c => c.City.Country).Include(a => a.Activities).ToListAsync();

            var model = (withChildren != null && withChildren == true)?await ApplicationDbContext.Budgets.ToListAsync():await ApplicationDbContext.Budgets.ToListAsync();

           
            if (model == null)
            {
                var msg = String.Format(FAILGETENTITIES);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }

        [HttpGet("{budgetId:int}", Name = "GetBudgetById")]
        public async Task<IActionResult> GetBudgetById(Int16 budgetId)
        {
            var model = await ApplicationDbContext.Budgets.FirstOrDefaultAsync(o => o.Id == budgetId);

            if (model == null)
            {
                var msg = String.Format(FAILGETENTITYBYID, budgetId);
                return NotFound(msg);
            }
            return new OkObjectResult(model);
        }
    }
}
