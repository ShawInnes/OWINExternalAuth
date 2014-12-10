using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Serilog;

namespace MiniWeb.Models
{
    public class ApplicationUser : ClaimsIdentity, IUser<string>
    {
        public ApplicationUser()
        {
            SecurityStamp = Guid.NewGuid().ToString("N");
        }

        public virtual string Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual double Balance { get; set; }
        public virtual string SecurityStamp { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            Log.Information("GenerateUserIdentityAsync {Id} {UserName}", Id, UserName);

            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
/*
            userIdentity.AddClaim(new Claim(ClaimTypes.Country, "AU", ClaimValueTypes.String));
            userIdentity.AddClaim(new Claim(ClaimTypes.DateOfBirth, new DateTime(1970, 12, 25).ToString("O"), ClaimValueTypes.DateTime));
            userIdentity.AddClaim(new Claim(ClaimTypes.UserData, this.Balance.ToString(), ClaimValueTypes.Double));
*/

            return userIdentity;
        }
    }
}