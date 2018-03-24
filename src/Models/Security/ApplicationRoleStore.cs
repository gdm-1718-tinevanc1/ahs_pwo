/*using System;
using OpenIddict;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Models.Security
{
    public class ApplicationRoleStore : RoleManager<Role, ApplicationDbContext, int, UserRole, RoleClaim> 
    {
        protected override RoleClaim CreateRoleClaim(Role role, Claim claim)
        {
            return new RoleClaim
            {
                RoleId = role.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            };
        }
    }
}*/