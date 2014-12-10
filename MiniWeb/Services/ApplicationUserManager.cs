using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using MiniWeb.Models;
using Newtonsoft.Json;
using Serilog;

namespace MiniWeb
{
    public class ApplicationUserManager : UserManager<ApplicationUser, string>
    {
        private readonly IUserCache cache;
        private readonly HttpClient client;

        public ApplicationUserManager(ServicedUserStore store)
            : base(store)
        {
            cache = DependencyResolver.Current.GetService<IUserCache>();
            client = new HttpClient();
        }

        public override async Task<ApplicationUser> FindByIdAsync(string userId)
        {
            Log.Information("FindByIdAsync {UserId}", userId);

            if (cache.GetCache().ContainsKey(userId))
            {
                Log.Information("Cache Hit");
                return await Task.FromResult(cache.GetCache()[userId]);
            }

            return await Task.FromResult<ApplicationUser>(null);
        }

        public override async Task<ApplicationUser> FindAsync(string userName, string password)
        {
            Log.Information("FindAsync {UserName}", userName);

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://api.auth.localhost/api/auth");

                var traceId = Guid.NewGuid(); // until I can get ETW sorted

                LoginViewModel vm = new LoginViewModel()
                {
                    UserName = userName,
                    Password = password,
                    CorrelationId = traceId
                };

                request.Content = new StringContent(JsonConvert.SerializeObject(vm), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.SendAsync(request).Result;

                if (response.IsSuccessStatusCode)
                {

                    var result = await Task.FromResult(response.Content.ReadAsAsync<ApplicationUser>(new List<MediaTypeFormatter>
                        {
                            new JsonMediaTypeFormatter()
                        }).Result);

                    cache.GetCache().TryAdd(result.Id, result);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "FindAsync Error");
            }

            return await Task.FromResult<ApplicationUser>(null);
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new ServicedUserStore());

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            IDataProtectionProvider dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }
    }
}