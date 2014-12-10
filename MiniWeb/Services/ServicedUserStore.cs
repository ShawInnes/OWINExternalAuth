using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Provider;
using MiniWeb.Models;
using Serilog;

namespace MiniWeb
{
    public class ServicedUserStore : IUserStore<ApplicationUser>, IUserSecurityStampStore<ApplicationUser>
    {
        public void Dispose()
        {

        }
        
        public async Task CreateAsync(ApplicationUser user)
        {
            Log.Information("ServicedUserStore.CreateAsync");

            return;
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            Log.Information("ServicedUserStore.UpdateAsync");

            return;
        }

        public async Task DeleteAsync(ApplicationUser user)
        {
            Log.Information("ServicedUserStore.DeleteAsync");

            return;
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId)
        {
            Log.Information("ServicedUserStore.FindByIdAsync");

            return await Task.FromResult<ApplicationUser>(null);
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            Log.Information("ServicedUserStore.FindByNameAsync");

            return await Task.FromResult<ApplicationUser>(null);
        }

        public async Task SetSecurityStampAsync(ApplicationUser user, string stamp)
        {
            Log.Information("ServicedUserStore.SetSecurityStampAsync");
            
            user.SecurityStamp = stamp;

            return;
        }

        public async Task<string> GetSecurityStampAsync(ApplicationUser user)
        {
            Log.Information("ServicedUserStore.GetSecurityStampAsync");

            return await Task.FromResult<string>(user.SecurityStamp);
        }
    }
}