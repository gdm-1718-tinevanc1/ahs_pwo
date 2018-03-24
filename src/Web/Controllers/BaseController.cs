using Microsoft.AspNetCore.Mvc;
using OpenIddict;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Database;
using Models;
using Models.Security;

namespace Web.Controllers {

    public class BaseController : Controller {
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }
        public BaseController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager) {
            ApplicationDbContext = applicationDbContext;
            UserManager = userManager;
        }
    }
}