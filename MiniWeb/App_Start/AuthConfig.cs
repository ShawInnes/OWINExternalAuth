using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Google;
using Owin;
using System;
using MiniWeb.Models;

namespace MiniWeb
{
    public static class AuthConfig
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public static void Register(IAppBuilder app)
        {
            // Configure the user manager to use a single instance per request
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                SlidingExpiration = true,
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager,ApplicationUser, string>(
                        validateInterval: TimeSpan.FromSeconds(30),
                        regenerateIdentityCallback: (ApplicationUserManager manager, ApplicationUser user) => user.GenerateUserIdentityAsync(manager),
                        getUserIdCallback: (id) => (string)id.GetUserId())
                }
            });

            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}